using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoRentalStoreSystem.DAL;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Repositories;
using VideoRentalStoreSystem.UI.Models;
using VideoRentalStoreSystem.BLL;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Reflection;
using VideoRentalStoreSystem.DAL.Models;
using System.Data;

namespace VideoRentalStoreSystem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DBVRContext context;
        private CustomerRepository customerRepository;
        private TitleDiskResponsitory titleResponsitory;
        private TypeDiskRepository typeDiskRepository;
        private DiskRepository diskRepository;
        private ManageRentalRecordDetail manageRentalRecordDetail;
        private ManageRentalRecord manageRentalRecord;
        private ManageReturnDisk manageReturnRepository;
        private RentalRecordDetailRepository detailRepository;
        public MainWindow()
        {
            InitializeComponent();

            checkLogin = new delegateCheckLogin(CheckLogin);

            context = new DBVRContext();
            detailRepository = new RentalRecordDetailRepository(context);
            customerRepository = new CustomerRepository(context);
            titleResponsitory = new TitleDiskResponsitory(context);
            typeDiskRepository = new TypeDiskRepository(context);
            diskRepository = new DiskRepository(context);
            manageReturnRepository = new ManageReturnDisk();
            LoadTabRental();
            dtpReturnDiskDateActualReturn.DisplayDateEnd = DateTime.Now;
            Block(false);
        }

        #region Tab focus
        private int indexTab = -1;
        private void btnTabItemRental_Click(object sender, RoutedEventArgs e)
        {
            if (indexTab != 0)
            {
                tabControl.SelectedIndex = 0;
                LoadTabRental();
                SetForegroundTab(Colors.Blue, Colors.Black, Colors.Black,
                    Colors.Black, Colors.Black, Colors.Black, Colors.Black);
                indexTab = 0;
            }
        }
        private void btnTabItemReturnDisk_Click(object sender, RoutedEventArgs e)
        {
            if (indexTab != 1)
            {
                tabControl.SelectedIndex = 1;
                LoadTabReturnDisk();
                SetForegroundTab(Colors.Black, Colors.Blue, Colors.Black,
                    Colors.Black, Colors.Black, Colors.Black, Colors.Black);
                indexTab = 1;
            }
        }
        private void btnTabItemViewInfoDisk_Click(object sender, RoutedEventArgs e)
        {
            if (indexTab != 2)
            {
                tabControl.SelectedIndex = 2;
                LoadTabViewInfoDisk();
                SetForegroundTab(Colors.Black, Colors.Black, Colors.Blue,
                    Colors.Black, Colors.Black, Colors.Black, Colors.Black);
                indexTab = 2;
            }
        }
        private void btnTabItemManageCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (indexTab != 3)
            {
                tabControl.SelectedIndex = 3;
                LoadTabManageCustomer();
                SetForegroundTab(Colors.Black, Colors.Black, Colors.Black,
                    Colors.Blue, Colors.Black, Colors.Black, Colors.Black);
                indexTab = 3;
            }
        }
        private void btnTabItemManageTitleDisk_Click(object sender, RoutedEventArgs e)
        {
            if (indexTab != 4)
            {
                tabControl.SelectedIndex = 4;
                LoadTabManageTitle();
                SetForegroundTab(Colors.Black, Colors.Black, Colors.Black,
                    Colors.Black, Colors.Blue, Colors.Black, Colors.Black);
                indexTab = 4;
            }
        }
        private void btnTabItemManageInventory_Click(object sender, RoutedEventArgs e)
        {
            if (indexTab != 5)
            {
                tabControl.SelectedIndex = 5;
                LoadTabManageInventory();
                SetForegroundTab(Colors.Black, Colors.Black, Colors.Black,
                    Colors.Black, Colors.Black, Colors.Blue, Colors.Black);
                indexTab = 5;
            }
        }
        private void btnTabItemReports_Click(object sender, RoutedEventArgs e)
        {
            if (indexTab != 6)
            {
                tabControl.SelectedIndex = 6;
                SetForegroundTab(Colors.Black, Colors.Black, Colors.Black,
                    Colors.Black, Colors.Black, Colors.Black, Colors.Blue);
                indexTab = 6;
            }
        }
        private void SetForegroundTab(Color col1, Color col2, Color col3, Color col4, Color col5, Color col6, Color col7)
        {
            btnTabItemRental.Foreground = new SolidColorBrush(col1);
            btnTabItemReturnDisk.Foreground = new SolidColorBrush(col2);
            btnTabItemViewInfoDisk.Foreground = new SolidColorBrush(col3);
            btnTabItemManageCustomer.Foreground = new SolidColorBrush(col4);
            btnTabItemManageTitleDisk.Foreground = new SolidColorBrush(col5);
            btnTabItemManageInventory.Foreground = new SolidColorBrush(col6);
            btnTabItemReports.Foreground = new SolidColorBrush(col7);
        }
        #endregion

        #region Rental - tab "CHO THUÊ ĐĨA"
        List<RentalRecordDetail> lstDiskChoosed;
        List<Disk> lstDiskCurrent;
        List<Disk> lstDiskRental;
        private void LoadTabRental()
        {
            SetForegroundTab(Colors.Blue, Colors.Black, Colors.Black, Colors.Black, Colors.Black, Colors.Black, Colors.Black);

            lstDiskRental = new List<Disk>();
            lstDiskChoosed = new List<RentalRecordDetail>();
            lstDiskCurrent = new List<Disk>();

            lvwRentalInfoDisk.ItemsSource = null;
            lbxRentalResultCusId.ItemsSource = null;
            lbxRentalResultDiskId.ItemsSource = null;

            lbxRentalResultCusId.ItemsSource = customerRepository.GetCustomers().ToList();
            lstDiskCurrent = diskRepository.Get(StatusOfDisk.ON_SHELF).ToList();
            lbxRentalResultDiskId.ItemsSource = lstDiskCurrent;

            lblRentalIdCus.Content = "";
            lblRentalNameCus.Content = "";
            lblRentalPhoneNumberCus.Content = "";
            lblRentalAddressCus.Content = "";
            lblRentalTotal.Content = "";
            lblRentalTotalChargeCus.Content = "";
        }
        private void tbxFindRentalCusId_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbxRentalResultDiskId.ItemsSource = null;
            lbxRentalResultCusId.ItemsSource = customerRepository.FindCustomerID(tbxFindRentalCusId.Text.Trim()).ToList();
        }
        private void lbxRentalResultCusId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxRentalResultCusId.SelectedItem != null)
            {
                if (lbxRentalResultCusId.SelectedItem is Customer)
                {
                    CheckLateCharge checkLateCharge = new CheckLateCharge();
                    Customer cus = (Customer)lbxRentalResultCusId.SelectedItem;
                    lblRentalIdCus.Content = cus.CustomerID;
                    lblRentalNameCus.Content = cus.Name;
                    lblRentalAddressCus.Content = cus.Address;
                    lblRentalPhoneNumberCus.Content = cus.PhoneNumber;
                    checkLateCharge.Check(cus.CustomerID);
                    lblRentalTotalChargeCus.Content = checkLateCharge.GetTotalLateCharge();
                }
            }
        }
        private void tbxFindRentalDiskId_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstDiskCurrent = new List<Disk>();
            lbxRentalResultDiskId.ItemsSource = null;
            lstDiskCurrent = diskRepository.Get(tbxFindRentalDiskId.Text.Trim(), StatusOfDisk.ON_SHELF).ToList();
            lbxRentalResultDiskId.ItemsSource = lstDiskCurrent;
        }
        private void btnGetDisk_Click(object sender, RoutedEventArgs e)
        {
            if (lbxRentalResultDiskId.SelectedItems.Count > 0)
            {
                if (lstDiskChoosed != null)
                {
                    if (manageRentalRecordDetail == null)
                    {
                        manageRentalRecordDetail = new ManageRentalRecordDetail();
                        manageRentalRecord = new ManageRentalRecord(context);
                    }
                    foreach (var item in lbxRentalResultDiskId.SelectedItems)
                    {
                        if (item is Disk)
                        {
                            Disk disk = (Disk)item;
                            if (!lstDiskRental.Contains(disk))
                                lstDiskRental.Add(disk);
                            lstDiskCurrent.Remove(lstDiskCurrent.Find(x => x.DiskID == disk.DiskID));
                        }
                    }
                    lbxRentalResultDiskId.ItemsSource = null;
                    lbxRentalResultDiskId.ItemsSource = lstDiskCurrent.OrderBy(x => x.DiskID);
                    lvwRentalInfoDisk.ItemsSource = null;
                    if (lstDiskChoosed.Count <= 0)
                        lstDiskChoosed = manageRentalRecordDetail.Initialize(lstDiskRental);
                    else
                        lstDiskChoosed = manageRentalRecordDetail.Modify(lstDiskRental, lstDiskChoosed);
                    lvwRentalInfoDisk.ItemsSource = lstDiskChoosed.OrderBy(x => x.DiskID);
                    lblRentalTotal.Content = manageRentalRecord.CalculateTotalPayment(lstDiskChoosed) + " (VND)";
                }
            }
            else
            {
                MessageBox.Show(VRSSMessage.NotChooseDiskRent);
            }
        }
        private void btnRemoveDisk_Click(object sender, RoutedEventArgs e)
        {
            if (lvwRentalInfoDisk.SelectedItems.Count > 0)
            {
                if (lstDiskChoosed != null)
                {
                    foreach (var item in lvwRentalInfoDisk.SelectedItems)
                    {
                        if (item is RentalRecordDetail)
                        {
                            RentalRecordDetail detail = (RentalRecordDetail)item;
                            if (!lstDiskCurrent.Contains(lstDiskRental.Find(x => x.DiskID == detail.DiskID)))
                                lstDiskCurrent.Add(lstDiskRental.Find(x => x.DiskID == detail.DiskID));
                            lstDiskRental.Remove(lstDiskRental.Find(x => x.DiskID == detail.DiskID));
                            lstDiskChoosed.Remove(detail);
                        }
                    }
                    lbxRentalResultDiskId.ItemsSource = null;
                    lbxRentalResultDiskId.ItemsSource = lstDiskCurrent.OrderBy(x => x.DiskID);
                    lvwRentalInfoDisk.ItemsSource = null;
                    lstDiskChoosed = manageRentalRecordDetail.Modify(lstDiskRental, lstDiskChoosed);
                    lvwRentalInfoDisk.ItemsSource = lstDiskChoosed.OrderBy(x => x.DiskID);
                    lblRentalTotal.Content = manageRentalRecord.CalculateTotalPayment(lstDiskChoosed) + " (VND)";
                }
            }
            else
            {
                MessageBox.Show(VRSSMessage.NotChooseDiskRemove);
            }
        }
        private void btnRentalCalculation_Click(object sender, RoutedEventArgs e)
        {
            int customerId;
            if (string.IsNullOrEmpty(lblRentalIdCus.Content.ToString()) || !int.TryParse(lblRentalIdCus.Content.ToString(), out customerId))
            {
                customerId = 0;
                MessageBox.Show(VRSSMessage.NotChooseCustomerRent, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (lstDiskChoosed.Count > 0)
            {
                RentalRecord rentalRecord = manageRentalRecord.Initialize(customerId, lstDiskChoosed);
                if (rentalRecord == null)
                {
                    MessageBox.Show(VRSSMessage.ErrorCreateRecord, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (manageRentalRecord.AddRentalRecord(rentalRecord))
                {
                    MessageBox.Show(VRSSMessage.SavedInformation, "", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadTabRental();
                    return;
                }
                else
                {
                    MessageBox.Show(VRSSMessage.ErrorCreateRecord, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show(VRSSMessage.NoDiskInListRent, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }
        private void btnRentalNew_Click(object sender, RoutedEventArgs e)
        {
            LoadTabRental();
        }
        #endregion

        #region Return disk - tab "TRẢ ĐĨA"
        List<Disk> lstDiskIdReturn;
        private void LoadTabReturnDisk()
        {
            lstDiskIdReturn = new List<Disk>();
            lbxReturnResultSearch.ItemsSource = null;
            lbxReturnResultSearch.ItemsSource = diskRepository.Get(StatusOfDisk.RENTED).ToList();
            dtpReturnDiskDateActualReturn.SelectedDate = DateTime.Now;
        }
        private void tbxReturnSearchDiskID_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadTabReturnDisk();
            lstDiskIdReturn = diskRepository.Get(tbxReturnSearchDiskID.Text.Trim(), StatusOfDisk.RENTED).ToList();
            lbxReturnResultSearch.ItemsSource = lstDiskIdReturn;
        }
        private void btnReturnDisk_Click(object sender, RoutedEventArgs e)
        {
            if (lbxReturnResultSearch.SelectedItem != null)
            {
                if (lbxReturnResultSearch.SelectedItem is Disk)
                {
                    Disk disk = (Disk)lbxReturnResultSearch.SelectedItem;
                    int result = manageReturnRepository.ReturnDisk(disk, dtpReturnDiskDateActualReturn.SelectedDate.Value);
                    if (result >= 0)
                    {
                        //return success
                        if (result == 0)
                        {
                            MessageBox.Show(VRSSMessage.DiskReturnedSuccess, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            lblReturnDiskMessage.Content = "";
                        }
                        else if(result == 3)
                        {
                            MessageBox.Show(VRSSMessage.DateReturnActualFail, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            if (MessageBox.Show(VRSSMessage.DiskReturnedSuccessAndLateCharge, "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                            {
                                lvwViewReturnDiskLateCharge.ItemsSource = null;
                                tbxReturnDiskCustomerID.Text = detailRepository.GetCustomerByDiskLateCharge(disk.DiskID).CustomerID+"";
                                lvwViewReturnDiskLateCharge.ItemsSource = detailRepository.GetInformationLateCharges(detailRepository.GetCustomerByDiskLateCharge(disk.DiskID).CustomerID);
                            }
                            lblReturnDiskMessage.Content = "Đĩa có mã số " + disk.DiskID + " trễ hạn trả.";
                        }
                        LoadTabReturnDisk();
                        tbxReturnSearchDiskID.Focus();
                        tbxReturnSearchDiskID.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show(VRSSMessage.DiskReturnFail, "", MessageBoxButton.OK, MessageBoxImage.Information);
                        tbxReturnSearchDiskID.Focus();
                        tbxReturnSearchDiskID.SelectAll();
                        lblReturnDiskMessage.Content = "";
                    }
                }
            }
            else
            {
                MessageBox.Show(VRSSMessage.NotChooseDiskReturn, "", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }
        private void btnReturnDiskSearchCustomer_Click(object sender, RoutedEventArgs e)
        { 
            if(tbxReturnDiskCustomerID.Text !="" && IsNumber(tbxReturnDiskCustomerID.Text))
            {
                lvwViewReturnDiskLateCharge.ItemsSource = null;
                lvwViewReturnDiskLateCharge.ItemsSource = detailRepository.GetInformationLateCharges(Convert.ToInt16(tbxReturnDiskCustomerID.Text));
            }
        }
        private void lvwViewReturnDiskLateCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double totalPrice = 0;
            if(lvwViewReturnDiskLateCharge.SelectedIndex !=-1)
            {
                foreach(RentalRecordDetail detail in lvwViewReturnDiskLateCharge.SelectedItems)
                {
                    totalPrice +=Convert.ToInt32(detail.LateCharge);
                }
            }
            lblReturnDiskTotalPrice.Content = totalPrice + "";
        }
        private void btnReturnDiskPayLateCharge_Click(object sender, RoutedEventArgs e)
        {
            List<RentalRecordDetail> rentalRecordDetails = new List<RentalRecordDetail>();
            foreach (RentalRecordDetail detail in lvwViewReturnDiskLateCharge.Items)
            {
                rentalRecordDetails.Add(detail);
            }
            if (lvwViewReturnDiskLateCharge.SelectedIndex != -1)
            {
                foreach (RentalRecordDetail detail in lvwViewReturnDiskLateCharge.SelectedItems)
                {
                    manageReturnRepository.UpdateLateCharge(detail);
                    rentalRecordDetails.Remove(detail);
                }
                lvwViewReturnDiskLateCharge.ItemsSource = rentalRecordDetails;
            }

        }
        #endregion

        #region Search Disk - tab "XEM THÔNG TIN ĐĨA"
        private void LoadTabViewInfoDisk()
        {
            lbxViewInfoDiskListTitleDisk.ItemsSource = null;
            lvwViewInfoDiskListDisk.ItemsSource = null;
            lbxViewInfoDiskListTitleDisk.ItemsSource = titleResponsitory.GetAll().ToList();
            if (lbxViewInfoDiskListTitleDisk.ItemsSource != null)
            {
                lbxViewInfoDiskListTitleDisk.SelectedIndex = 0;
            }
        }
        private void txtViewInfoDiskSearchTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtViewInfoDiskSearchTitle.Text.ToString()))
            {
                LoadTabViewInfoDisk();
            }
            else
            {
                lbxViewInfoDiskListTitleDisk.ItemsSource = null;
                lbxViewInfoDiskListTitleDisk.ItemsSource = titleResponsitory.Find(txtViewInfoDiskSearchTitle.Text.Trim()).ToList();
                if (lbxViewInfoDiskListTitleDisk.ItemsSource != null)
                {
                    lbxViewInfoDiskListTitleDisk.SelectedIndex = 0;
                }
            }
        }
        private void lbxViewInfoDiskListTitleDisk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxViewInfoDiskListTitleDisk.SelectedItem != null)
            {
                if (lbxViewInfoDiskListTitleDisk.SelectedItem is TitleDisk)
                {
                    TitleDisk titleDisk = (TitleDisk)lbxViewInfoDiskListTitleDisk.SelectedItem;
                    lvwViewInfoDiskListDisk.ItemsSource = null;
                    lvwViewInfoDiskListDisk.ItemsSource = diskRepository.GetInformationDisk(titleDisk.Title);
                    lblViewInfoDiskTitle.Content = titleDisk.Title;
                }
            }
        }
        #endregion

        #region Manage Customer - tab "QUẢN LÝ DANH SÁCH KHÁCH HÀNG"
        private void LoadTabManageCustomer()
        {
            lvwManageCustomerListCustomer.ItemsSource = null;
            lvwManageCustomerListCustomer.ItemsSource = customerRepository.GetCustomers().ToList();

            txtManageCustomerCustomerAddress.Clear();
            txtManageCustomerCustomerName.Clear();
            txtManageCustomerPhone.Clear();
            txtManageCustomerCustomerName.Focus();
        }
        /// <summary>
        /// check parameters input in tab "Quản lý khách hàng"
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputManageCustomer(TextBox Name, TextBox Address, TextBox PhoneNumber)
        {
            if (string.IsNullOrEmpty(Name.Text.Trim()))
            {
                MessageBox.Show(VRSSMessage.NameCustomerNotEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                Name.Focus();
                Name.SelectAll();
                return false;
            }
            if (!IsChar(Name.Text.Trim()))
            {
                MessageBox.Show(VRSSMessage.CustomerNameIsString, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                Name.Focus();
                Name.SelectAll();
                return false;
            }
            if (string.IsNullOrEmpty(Address.Text.Trim()))
            {
                MessageBox.Show(VRSSMessage.AddressNotEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                Address.Focus();
                Address.SelectAll();
                return false;
            }
            if (string.IsNullOrEmpty(Address.Text.Trim()))
            {
                MessageBox.Show(VRSSMessage.PhoneNumberNotEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                Address.Focus();
                Address.SelectAll();
                return false;
            }
            if (!IsNumber(PhoneNumber.Text.Trim()) || PhoneNumber.Text.Trim().Length < 10 || PhoneNumber.Text.Trim().Length > 11)
            {
                MessageBox.Show(VRSSMessage.PhoneNumberLenght, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                PhoneNumber.Focus();
                PhoneNumber.SelectAll();
                return false;
            }
            if (int.Parse(PhoneNumber.Text.Substring(0, 1)) != 0)
            {
                MessageBox.Show(VRSSMessage.PhoneNumberBeginWith0, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                PhoneNumber.Focus();
                PhoneNumber.SelectAll();
                return false;
            }
            return true;
        }
        private void btnManageCustomerAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputManageCustomer(txtManageCustomerCustomerName, txtManageCustomerCustomerAddress, txtManageCustomerPhone))
            {
                Customer customer = new Customer();
                customer.Name = txtManageCustomerCustomerName.Text.Trim();
                customer.Address = txtManageCustomerCustomerAddress.Text.Trim();
                customer.PhoneNumber = txtManageCustomerPhone.Text.Trim();
                customer.IsDeleted = 0;
                ManageCustomer manage = new ManageCustomer();
                manage.AddCustomer(customer);
                LoadTabManageCustomer();
            }
        }
        private void btnManageCustomerDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (lvwManageCustomerListCustomer.SelectedIndex != -1)
            {
                Customer customer = new Customer();
                customer = (Customer)lvwManageCustomerListCustomer.SelectedValue;
                ManageCustomer manageCustomer = new ManageCustomer();
                string alert = manageCustomer.isDelete(customer.CustomerID);
                if (alert != "")
                {
                    if (MessageBox.Show(VRSSMessage.DeleteCustomerFail.Replace("<latecharge>", alert), "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                    {
                        manageCustomer.DeleteCustomer(customer.CustomerID);
                        txtManageCustomerUpdateCustomerID.Clear();
                        txtManageCustomerUpdateCustomerName.Clear();
                        txtManageCustomerUpdateCustomerAddress.Clear();
                        txtManageCustomerUpdatePhone.Clear();
                        LoadTabManageCustomer();
                    }
                    return;

                }
                else
                {
                    manageCustomer.DeleteCustomer(customer.CustomerID);
                    txtManageCustomerUpdateCustomerID.Clear();
                    txtManageCustomerUpdateCustomerName.Clear();
                    txtManageCustomerUpdateCustomerAddress.Clear();
                    txtManageCustomerUpdatePhone.Clear();
                    LoadTabManageCustomer();
                }

            }

        }
        private void lvwManageCustomerListCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvwManageCustomerListCustomer.SelectedIndex != -1)
            {
                Customer customer = new Customer();
                customer = (Customer)lvwManageCustomerListCustomer.SelectedValue;
                txtManageCustomerUpdateCustomerID.Text = customer.CustomerID.ToString();
                txtManageCustomerUpdateCustomerName.Text = customer.Name;
                txtManageCustomerUpdateCustomerAddress.Text = customer.Address;
                txtManageCustomerUpdatePhone.Text = customer.PhoneNumber;
            }


        }
        private void btnManageCustomerUpdateAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (txtManageCustomerUpdateCustomerID.Text != "" && ValidateInputManageCustomer(txtManageCustomerUpdateCustomerName, txtManageCustomerUpdateCustomerAddress, txtManageCustomerUpdatePhone))
            {
                Customer customer = new Customer();
                customer.CustomerID = Convert.ToInt16(txtManageCustomerUpdateCustomerID.Text);
                customer.Name = txtManageCustomerUpdateCustomerName.Text;
                customer.Address = txtManageCustomerUpdateCustomerAddress.Text;
                customer.PhoneNumber = txtManageCustomerUpdatePhone.Text;
                if (customer != null)
                {
                    customerRepository.UpdateCustomer(customer);
                    LoadTabManageCustomer();
                }
            }

        }
        private void txtManageCustomerFindCustomerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtManageCustomerFindCustomerName.Text != "")
            {
                lvwManageCustomerListCustomer.ItemsSource = null;
                lvwManageCustomerListCustomer.ItemsSource = customerRepository.FindCustomerName(txtManageCustomerFindCustomerName.Text).ToList();
            }
            else
            {
                lvwManageCustomerListCustomer.ItemsSource = null;
                lvwManageCustomerListCustomer.ItemsSource = customerRepository.GetCustomers().ToList();
            }
        }
        #endregion

        #region Manage Title - tab "QUẢN LÝ TỰA ĐỀ"
        private void LoadTabManageTitle()
        {
            List<string> typeDisks = new List<string>();
            typeDisks = typeDiskRepository.GetAll().Select(x => x.TypeName).ToList();
            cboManageTitleTypeTitle.ItemsSource = typeDisks;
            cboManageTitleUpdateTypeTitle.ItemsSource = typeDisks;
            TextPeriodCostChanged();
            LoadListTypeManageTitle();
        }
        private void LoadListTypeManageTitle()
        {
            lvwManageTitleListTitle.ItemsSource = null;
            lvwManageTitleListTitle.ItemsSource = titleResponsitory.GetAll().ToList();
        }
        /// <summary>
        /// Check parameters input of tab "Quản lý tựa đề"
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputManageTitle()
        {
            foreach (char c in txtManageTitleTitleName.Text.Trim())
            {

                if (!char.IsLetter(c) && !char.IsNumber(c) && c != ' ')
                {
                    MessageBox.Show(VRSSMessage.TitileNotSpecialCharacter, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtManageTitleTitleName.Focus();
                    txtManageTitleTitleName.SelectAll();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtManageTitleTitleName.Text.Trim()))
            {
                MessageBox.Show(VRSSMessage.TitleNotEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtManageTitleTitleName.Focus();
                txtManageTitleTitleName.SelectAll();
                return false;
            }
            if (cboManageTitleTypeTitle.Text == "")
            {
                MessageBox.Show(VRSSMessage.ChooseTypeOfTitle, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }
        private void btnManageTitleAddTitle_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputManageTitle())
            {
                TitleDisk title = new TitleDisk();
                title.Title = txtManageTitleTitleName.Text.Trim();
                title.TypeName = cboManageTitleTypeTitle.Text.Trim();
                ManageTitle manageTitle = new ManageTitle();
                try
                {
                    manageTitle.AddTitle(title);
                    txtManageTitleTitleName.Clear();
                    LoadTabManageTitle();
                }
                catch (Exception)
                {
                    MessageBox.Show(VRSSMessage.AddFail, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
        }
        private void btnManageTitleUpdateCostTitle_Click(object sender, RoutedEventArgs e)
        {
            if (validateUpdateTypeTitle())
            {
                TypeDisk typeDisk = new TypeDisk();
                typeDisk.TypeName = cboManageTitleUpdateTypeTitle.SelectedValue.ToString();
                typeDisk.Cost = Convert.ToDouble(txtManageTitleCostTitleName.Text);
                typeDisk.Period = Convert.ToInt16(txtManageTitlePeriodTitleName.Text);
                typeDisk.LateCharge = Convert.ToDouble(txtManageTitleLateChargeTitleName.Text);
                if (!typeDiskRepository.Update(typeDisk))
                {
                    MessageBox.Show(VRSSMessage.UdpdatedFail, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                LoadTabManageTitle();

            }
        }
        private bool validateUpdateTypeTitle()
        {
            if (!IsNumber(txtManageTitleCostTitleName.Text))
            {
                MessageBox.Show(VRSSMessage.CostRentalIsNumber, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtManageTitleCostTitleName.Clear();
                txtManageTitleCostTitleName.Focus();
                return false;
            }
            else if (!IsNumber(txtManageTitlePeriodTitleName.Text))
            {
                MessageBox.Show(VRSSMessage.PeriodRentalIsNumber, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtManageTitlePeriodTitleName.Clear();
                txtManageTitlePeriodTitleName.Focus();
                return false;
            }
            else if (!IsNumber(txtManageTitleLateChargeTitleName.Text))
            {
                MessageBox.Show(VRSSMessage.CostLateChargeRentalIsNumber, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtManageTitleLateChargeTitleName.Clear();
                txtManageTitleLateChargeTitleName.Focus();
                return false;
            }
            return true;
        }
        private void cboManageTitleCostTypeTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextPeriodCostChanged();
        }
        private void TextPeriodCostChanged()
        {
            string typeName = cboManageTitleUpdateTypeTitle.SelectedValue.ToString();
            if (typeName != "")
            {
                TypeDisk typeDisk = new TypeDisk();
                typeDisk = typeDiskRepository.GetAll().Where(x => x.TypeName == typeName).FirstOrDefault();
                txtManageTitleCostTitleName.Text = typeDisk.Cost.ToString();
                txtManageTitlePeriodTitleName.Text = typeDisk.Period.ToString();
                txtManageTitleLateChargeTitleName.Text = typeDisk.LateCharge.ToString();
            }

        }
        private void txtManageTitleFindTitleName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtManageTitleFindTitleName.Text != "")
            {
                lvwManageTitleListTitle.ItemsSource = null;
                lvwManageTitleListTitle.ItemsSource = titleResponsitory.Find(txtManageTitleFindTitleName.Text).ToList();
            }
            else
            {
                lvwManageTitleListTitle.ItemsSource = null;
                lvwManageTitleListTitle.ItemsSource = titleResponsitory.GetAll().ToList();
            }

        }
        private void btnManageTitleDeleteTitle_Click(object sender, RoutedEventArgs e)
        {
            ManageTitle manageTitle = new ManageTitle();
            foreach (var item in lvwManageTitleListTitle.SelectedItems)
            {
                if (item is TitleDisk titleDisk)
                {
                    if (!manageTitle.IsDelete(titleDisk))
                    {
                        if (MessageBox.Show(VRSSMessage.DeleteTitleFail.Replace("<title>", titleDisk.Title), "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                            manageTitle.Delete(titleDisk.Title);
                    }
                    else
                    {
                        manageTitle.Delete(titleDisk.Title);
                    }
                }
            }
            LoadTabManageTitle();
        }
        #endregion

        #region Manage Disk tab "QUẢN LÝ TỒN KHO"
        public void LoadTabManageInventory()
        {
            lbxManageInventoryListBoxTitleDisk.ItemsSource = null;
            lbxManageInventoryListBoxTitleDisk.ItemsSource = titleResponsitory.GetAll().ToList();
            lvwManageInventoryListDeleteIdDiskOfTitle.ItemsSource = null;
            lvwManageInventoryListDeleteIdDiskOfTitle.ItemsSource = diskRepository.GetDisks().ToList();
            if (lbxManageInventoryListBoxTitleDisk.ItemsSource != null)
            {
                lbxManageInventoryListBoxTitleDisk.SelectedIndex = 0;
            }
        }
        private void tbxManageInventorySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxManageInventorySearch.Text.ToString()))
            {
                LoadTabManageInventory();
            }
            else
            {
                lbxManageInventoryListBoxTitleDisk.ItemsSource = null;
                lbxManageInventoryListBoxTitleDisk.ItemsSource = titleResponsitory.Find(tbxManageInventorySearch.Text.ToLower().Trim()).ToList();
                if (lbxManageInventoryListBoxTitleDisk.ItemsSource != null)
                {
                    lbxManageInventoryListBoxTitleDisk.SelectedIndex = 0;
                }
            }
        }
        private void lbxManageInventoryListBoxTitleDisk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbxManageInventoryListBoxTitleDisk_Load();
        }
        public void lbxManageInventoryListBoxTitleDisk_Load()
        {
            if (lbxManageInventoryListBoxTitleDisk.SelectedItem != null)
            {
                if (lbxManageInventoryListBoxTitleDisk.SelectedItem is TitleDisk)
                {
                    TitleDisk titleDisk = (TitleDisk)lbxManageInventoryListBoxTitleDisk.SelectedItem;
                    lblManageInventoryDiskTitle.Content = titleDisk.Title;
                    lvwManageInventoryListIdDiskOfTitle.ItemsSource = null;
                    lvwManageInventoryListIdDiskOfTitle.ItemsSource = diskRepository.GetByTitle(titleDisk.Title).ToList();
                }
            }
        }
        /// <summary>
        /// Check parameter input of tab "Quản lý tồn kho"
        /// </summary>
        /// <returns></returns>
        public bool CheckParamsManageInventory()
        {
            if (lblManageInventoryDiskTitle.Content.ToString().Trim() == "")
            {
                MessageBox.Show(VRSSMessage.YouMustChooseTitle, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (string.IsNullOrEmpty(tbxManageInventoryMountOfDisk.Text.Trim()))
            {
                MessageBox.Show(VRSSMessage.NumberOfDiskNotEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                tbxManageInventoryMountOfDisk.Focus();
                return false;
            }
            int num;
            if (!int.TryParse(tbxManageInventoryMountOfDisk.Text.Trim(), out num))
            {
                MessageBox.Show(VRSSMessage.NumberOfDishLargerThan0, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                tbxManageInventoryMountOfDisk.Focus();
                tbxManageInventoryMountOfDisk.SelectAll();
                return false;
            }
            else
            {
                if (num < 0 || num > 20)
                {
                    MessageBox.Show(VRSSMessage.NumberOfDishLargerThan0, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    tbxManageInventoryMountOfDisk.Focus();
                    tbxManageInventoryMountOfDisk.SelectAll();
                    return false;
                }
            }

            if (int.Parse(tbxManageInventoryMountOfDisk.Text.Trim()) < 0)
                return false;
            return true;
        }
        private void btnManageDiskAddDisk_Click(object sender, RoutedEventArgs e)
        {
            if (CheckParamsManageInventory())
            {
                ManageDisk manageDisk = new ManageDisk();
                manageDisk.AddDisk(int.Parse(tbxManageInventoryMountOfDisk.Text.Trim()), lblManageInventoryDiskTitle.Content.ToString());
                //lbxManageInventoryListBoxTitleDisk_Load();
                tbxManageInventoryMountOfDisk.Clear();
                LoadTabManageInventory();
            }
        }
        private void tbxManageInventoryFindDiskID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbxManageInventoryFindDiskID.Text != "")
            {
                TitleDisk titleDisk = (TitleDisk)lbxManageInventoryListBoxTitleDisk.SelectedItem;
                lvwManageInventoryListDeleteIdDiskOfTitle.ItemsSource = null;
                lvwManageInventoryListDeleteIdDiskOfTitle.ItemsSource = diskRepository.GetDisks().Where(x => x.DiskID.ToString().Contains(tbxManageInventoryFindDiskID.Text)).ToList();
            }
            else
            {
                TitleDisk titleDisk = (TitleDisk)lbxManageInventoryListBoxTitleDisk.SelectedItem;
                lvwManageInventoryListDeleteIdDiskOfTitle.ItemsSource = null;
                lvwManageInventoryListDeleteIdDiskOfTitle.ItemsSource = diskRepository.GetDisks().ToList();
            }
        }

        private void btnManageDiskDeleteDisk_Click(object sender, RoutedEventArgs e)
        {
            ManageDisk manageDisk = new ManageDisk();
            string alert = "";
            List<Disk> disks = new List<Disk>();
            if (lvwManageInventoryListDeleteIdDiskOfTitle.SelectedIndex != -1)
            {
                foreach (Disk disk in lvwManageInventoryListDeleteIdDiskOfTitle.SelectedItems)
                {
                    if (!manageDisk.isDelete(disk))
                        alert += " " + disk.DiskID;
                    disks.Add(disk);
                }
                if (alert != "")
                {
                    if (MessageBox.Show(VRSSMessage.DeleteDiskAmout.Replace("<id>", alert), "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                    {
                        manageDisk.DeleteDisk(disks);
                    }
                }
                else
                {
                    manageDisk.DeleteDisk(disks);
                }
                LoadTabManageInventory();
            }
        }
        #endregion

        #region reports - tab "BAO CAO"
        private bool isReportAllCustomer = false;
        private void btnReportCusReturnLate_Click(object sender, RoutedEventArgs e)
        {
            if (!isReportAllCustomer)
            {
                ReportDataSource reportDataSourceCus = new ReportDataSource();
                reportDataSourceCus.Name = "DataSetAllCustomer";
                reportDataSourceCus.Value = customerRepository.SelectCustomerCurrentlyOverdueDisk();
                ReportViewCustomer.LocalReport.DataSources.Add(reportDataSourceCus);
                ReportViewCustomer.LocalReport.ReportEmbeddedResource = "VideoRentalStoreSystem.UI.Reports.ReportAllCustomer.rdlc";
                ReportViewCustomer.RefreshReport();
                isReportAllCustomer = true;
            }
        }
        private bool isReportTitle = false;
        private void ReportViewTitle_Load(object sender, EventArgs e)
        {
            if (!isReportTitle)
            {
                ReportDataSource reportDataSourceTitle = new ReportDataSource();
                reportDataSourceTitle.Name = "DataSetTitle";
                reportDataSourceTitle.Value = diskRepository.GetInfoAllDisk();
                ReportViewTitle.LocalReport.DataSources.Add(reportDataSourceTitle);
                ReportViewTitle.LocalReport.ReportEmbeddedResource = "VideoRentalStoreSystem.UI.Reports.ReportTitle.rdlc";
                ReportViewTitle.RefreshReport();
                isReportTitle = true;
            }
        }
        #endregion

        #region Authentication
        public delegate void delegateCheckLogin(int statusLogin);
        public delegateCheckLogin checkLogin;
        public void CheckLogin(int statusLogin)
        {
            if (statusLogin == 1)
            {
                Block(true);
            }
            else if (statusLogin == -1)
            {
                this.Close();
            }
            if (!SecurityService.IsBlock() && !SecurityService.IsLogin())
            {
                Block(false);
            }
        }
        public void Block(bool value)
        {
            btnManageTitleAddTitle.IsEnabled = value;

            btnManageCustomerDeleteCustomer.IsEnabled = value;
            btnManageTitleUpdateCostTitle.IsEnabled = value;
            btnManageTitleUpdateCostTitle.IsEnabled = value;
            btnLogin.IsEnabled = !value;
            btnLogout.IsEnabled = value;
            btnTabItemReports.IsEnabled = value;
            btnTabItemManageInventory.IsEnabled = value;
            if (value)
            {
                btnLogin.Visibility= Visibility.Hidden;
                btnLogout.Visibility = Visibility.Visible;
                btnTabItemReports.Visibility = Visibility.Visible;
                btnTabItemManageInventory.Visibility = Visibility.Visible;

            }
            else
            {
                btnLogin.Visibility = Visibility.Visible;
                btnLogout.Visibility = Visibility.Hidden;
                btnTabItemReports.Visibility = Visibility.Hidden;
                btnTabItemManageInventory.Visibility = Visibility.Hidden;
          
            }
            if (!value)
                if (indexTab != 0)
                {
                    tabControl.SelectedIndex = 0;
                    LoadTabRental();
                    SetForegroundTab(Colors.Blue, Colors.Black, Colors.Black,
                        Colors.Black, Colors.Black, Colors.Black, Colors.Black);
                    indexTab = 0;
                }
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (SecurityService.IsLogin())
            {
                MessageBox.Show(VRSSMessage.Logined);
                return;
            }
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.getParent(this);
            loginWindow.ShowDialog();
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            SecurityService.Logout();
            Block(false);
        }
        #endregion

        #region common
        /// <summary>
        /// Check string input is Number or not
        /// </summary>
        /// <param name="s">string input</param>
        /// <returns>true if is Number</returns> false or not
        public bool IsNumber(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) || char.IsSymbol(c))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Check string input is char or not
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsChar(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsLetter(c) && c != ' ')
                    return false;
            }
            return true;
        }



















        #endregion

       
    }
}
