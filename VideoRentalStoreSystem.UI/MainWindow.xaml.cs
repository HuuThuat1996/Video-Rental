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

        public MainWindow()
        {
            InitializeComponent();

            checkLogin = new delegateCheckLogin(CheckLogin);

            context = new DBVRContext();
            customerRepository = new CustomerRepository(context);
            titleResponsitory = new TitleDiskResponsitory(context);
            typeDiskRepository = new TypeDiskRepository(context);
            diskRepository = new DiskRepository(context);
            manageReturnRepository = new ManageReturnDisk(context);
            LoadTabRental();
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

            lbxRentalResultCusId.ItemsSource = customerRepository.GetAll().ToList();
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
            lbxRentalResultCusId.ItemsSource = customerRepository.Find(tbxFindRentalCusId.Text.Trim()).ToList();
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
                MessageBox.Show(VRSSMessage.MessageNum01);
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
                MessageBox.Show(VRSSMessage.MessageNum02);
            }
        }
        private void btnRentalCalculation_Click(object sender, RoutedEventArgs e)
        {
            int customerId;
            if (string.IsNullOrEmpty(lblRentalIdCus.Content.ToString()) || !int.TryParse(lblRentalIdCus.Content.ToString(), out customerId))
            {
                customerId = 0;
                MessageBox.Show(VRSSMessage.MessageNum03, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (lstDiskChoosed.Count > 0)
            {
                RentalRecord rentalRecord = manageRentalRecord.Initialize(customerId, lstDiskChoosed);
                if (rentalRecord == null)
                {
                    MessageBox.Show(VRSSMessage.MessageNum04, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (manageRentalRecord.AddRentalRecord(rentalRecord))
                {
                    MessageBox.Show(VRSSMessage.MessageNum05, "", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadTabRental();
                    return;
                }
                else
                {
                    MessageBox.Show(VRSSMessage.MessageNum04, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show(VRSSMessage.MessageNum06, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    int result = manageReturnRepository.ReturnDisk(disk);
                    if (result >= 0)
                    {
                        //return success

                        if (result == 0)
                        {
                            MessageBox.Show(VRSSMessage.MessageNum07, "", MessageBoxButton.OK, MessageBoxImage.Information);
                            lblReturnDiskMessage.Content = "";
                        }
                        else
                        {
                            MessageBox.Show(VRSSMessage.MessageNum22, "", MessageBoxButton.OK, MessageBoxImage.Information);
                            lblReturnDiskMessage.Content = "Đĩa có mã số " + disk.DiskID + " trễ hạn trả.";
                        }
                        LoadTabReturnDisk();
                        tbxReturnSearchDiskID.Focus();
                        tbxReturnSearchDiskID.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show(VRSSMessage.MessageNum08, "", MessageBoxButton.OK, MessageBoxImage.Information);
                        tbxReturnSearchDiskID.Focus();
                        tbxReturnSearchDiskID.SelectAll();
                        lblReturnDiskMessage.Content = "";
                    }
                }
            }
            else
            {
                MessageBox.Show(VRSSMessage.MessageNum09, "", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
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
            lvwManageCustomerListCustomer.ItemsSource = customerRepository.GetAll().ToList();

            txtManageCustomerCustomerAddress.Clear();
            txtManageCustomerCustomerName.Clear();
            txtManageCustomerPhone.Clear();
            txtManageCustomerCustomerName.Focus();
        }
        /// <summary>
        /// check parameters input in tab "Quản lý khách hàng"
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputManageCustomer()
        {
            if (string.IsNullOrEmpty(txtManageCustomerCustomerName.Text.Trim()))
            {
                MessageBox.Show(VRSSMessage.MessageNum10, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtManageCustomerCustomerName.Focus();
                txtManageCustomerCustomerName.SelectAll();
                return false;
            }
            if (!IsChar(txtManageCustomerCustomerName.Text.Trim()))
            {
                MessageBox.Show(VRSSMessage.MessageNum11, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtManageCustomerCustomerName.Focus();
                txtManageCustomerCustomerName.SelectAll();
                return false;
            }
            if (string.IsNullOrEmpty(txtManageCustomerCustomerAddress.Text.Trim()))
            {
                MessageBox.Show(VRSSMessage.MessageNum12, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtManageCustomerCustomerAddress.Focus();
                txtManageCustomerCustomerAddress.SelectAll();
                return false;
            }
            if (string.IsNullOrEmpty(txtManageCustomerPhone.Text.Trim()))
            {
                MessageBox.Show(VRSSMessage.MessageNum13, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtManageCustomerPhone.Focus();
                txtManageCustomerPhone.SelectAll();
                return false;
            }
            if (!IsNumber(txtManageCustomerPhone.Text.Trim()) || txtManageCustomerPhone.Text.Trim().Length < 10 || txtManageCustomerPhone.Text.Trim().Length > 11)
            {
                MessageBox.Show(VRSSMessage.MessageNum14, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtManageCustomerPhone.Focus();
                txtManageCustomerPhone.SelectAll();
                return false;
            }
            if (int.Parse(txtManageCustomerPhone.Text.Substring(0, 1)) != 0)
            {
                MessageBox.Show(VRSSMessage.MessageNum15, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtManageCustomerPhone.Focus();
                txtManageCustomerPhone.SelectAll();
                return false;
            }
            return true;
        }
        private void btnManageCustomerAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputManageCustomer())
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
        #endregion

        #region Manage Title - tab "QUẢN LÝ TỰA ĐỀ"
        private void LoadTabManageTitle()
        {
            cboManageTitleTypeTitle.ItemsSource = typeDiskRepository.GetAll().Select(x => x.TypeName).ToList();
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
                    MessageBox.Show(VRSSMessage.MessageNum16, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtManageTitleTitleName.Focus();
                    txtManageTitleTitleName.SelectAll();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtManageTitleTitleName.Text.Trim()))
            {
                MessageBox.Show(VRSSMessage.MessageNum17, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtManageTitleTitleName.Focus();
                txtManageTitleTitleName.SelectAll();
                return false;
            }
            if (cboManageTitleTypeTitle.Text == "")
            {
                MessageBox.Show(VRSSMessage.MessageNum18, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    MessageBox.Show(VRSSMessage.MessageNum27,"Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);
                    return;
                }
            }
        }
        #endregion

        #region Manage Disk tab "QUẢN LÝ TỒN KHO"
        public void LoadTabManageInventory()
        {
            lbxManageInventoryListBoxTitleDisk.ItemsSource = null;
            lbxManageInventoryListBoxTitleDisk.ItemsSource = titleResponsitory.GetAll().ToList();
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
                    if (lbxManageInventoryListBoxTitleDisk.Items.Count <= 0)
                    {

                    }
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
                MessageBox.Show(VRSSMessage.MessageNum19, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (string.IsNullOrEmpty(tbxManageInventoryMountOfDisk.Text.Trim()))
            {
                MessageBox.Show(VRSSMessage.MessageNum20, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                tbxManageInventoryMountOfDisk.Focus();
                return false;
            }
            int num;
            if (!int.TryParse(tbxManageInventoryMountOfDisk.Text.Trim(), out num))
            {
                MessageBox.Show(VRSSMessage.MessageNum21, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                tbxManageInventoryMountOfDisk.Focus();
                tbxManageInventoryMountOfDisk.SelectAll();
                return false;
            }
            else
            {
                if (num < 0 || num > 20)
                {
                    MessageBox.Show(VRSSMessage.MessageNum21, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
                lbxManageInventoryListBoxTitleDisk_Load();
                tbxManageInventoryMountOfDisk.Clear();
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
            btnTabItemReports.IsEnabled = value;
            btnTabItemManageInventory.IsEnabled = value;
            btnLogin.IsEnabled = !value;
            btnLogout.IsEnabled = value;
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
                MessageBox.Show(VRSSMessage.MessageNum24);
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
