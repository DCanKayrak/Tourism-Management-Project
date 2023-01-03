using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Turizm.Business.Concrete;
using Turizm.DataAccess.Concrete.EntityFramework;
using Turizm.Entities.Concrete;
using Turizm.Utils.Abstract;
using Turizm.Utils.Concrete;

namespace Turizm.UI
{
    public partial class MainMenu : Form
    {
        private AccountManager man = new AccountManager(new EfAccountDal());
        private TourManager _tourManager = new TourManager(new EfTourDal());
        private CustomerManager _customerManager = new CustomerManager(new EfCustomerDal());
        private LossManager _lossManager = new LossManager(new EfLossDal());
        private ISender sender = new MailTransactions();
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            LoadDatas();
            chartKarZarar.Titles.Add("Giderler");
        }
        
        #region PublicMethods
        private void btnExitMainMenu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void LoadDatas()
        {
            rbNo.Checked = true;
            LoadTours();
            LoadTourNames();
            LoadCustomers();
            LoadLosses();
            LoadCbxLossesFilter();
            LoadLossesTours();
        }
        #endregion

        #region Tours
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTours();
            LoadTourNames();
        }
        private void ClearTourSpecs()
        {
            tbxTourType.Text = "";
            tbxTourPrice.Text = "";
            tbxTourName.Text = "";
            tbxDesc.Text = "";
            dtpStartDate.Value = Convert.ToDateTime(DateTime.Now);
            dtpEndDate.Value = Convert.ToDateTime(DateTime.Now);
        }
        private void btnClearTourTbxs_Click(object sender, EventArgs e)
        {
            ClearTourSpecs();
        }
        private Tour FindTour(string tourName)
        {
            var result = _tourManager.GetWithName(tourName);
            if (result == null) 
            {
                throw new Exception("Lütfen geçerli bir tur giriniz.");
            }

            return result;
            
        }
        private void dgwTours_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Sütunu alıp tur ekleme menüsünden düzenlemeliyiz.

            Tour tour = new Tour()
            {
                Id = Convert.ToInt32(dgwTours.CurrentRow.Cells[0].Value),
                TourType = dgwTours.CurrentRow.Cells[1].Value.ToString(),
                Name = dgwTours.CurrentRow.Cells[2].Value.ToString(),
                Description = dgwTours.CurrentRow.Cells[3].Value.ToString(),
                isVisaRequired = dgwTours.CurrentRow.Cells[4].Value.ToString(),
                isAnyMealsIncluded = dgwTours.CurrentRow.Cells[5].Value.ToString(),
                StartDate = Convert.ToDateTime(dgwTours.CurrentRow.Cells[6].Value),
                EndDate = Convert.ToDateTime(dgwTours.CurrentRow.Cells[7].Value),
                Price = Convert.ToDecimal(dgwTours.CurrentRow.Cells[8].Value)
            };

            tabControlMain.SelectedIndex = 1;

            tbxDesc.Text = tour.Description;
            tbxTourPrice.Text = tour.Price.ToString();
            tbxTourType.Text = tour.TourType;
            tbxTourName.Text = tour.Name;
            dtpStartDate.Value = tour.StartDate;
            dtpEndDate.Value = tour.EndDate;
            cbxTourMeal.Text = tour.isAnyMealsIncluded;

            //MessageBox.Show(dgwTours.CurrentRow.Index.ToString()+" numaralı sütundasınız.");
        }

        private void btnDeleteTour_Click(object sender, EventArgs e)
        {
            var tour = _tourManager.GetWithId(Convert.ToInt32(dgwTours.CurrentRow.Cells[0].Value));
            _tourManager.Delete(tour);
            foreach(var item in _lossManager.getAllWithTourName(tour.Name))
            {
                _lossManager.Delete(item);
            }
            LoadDatas();
            MessageBox.Show(tour.Name + " isimli Tur Silindi");
        }

        private void EditTour(Tour tour)
        {

            tabControlMain.SelectedIndex = 3;
        }

        private void tbxTourSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbxTourSearch.Text == "")
            {
                LoadTours();
            }
            else
            {
                switch (cbxTourSearch.SelectedIndex)
                {
                    case 0:
                        dgwTours.DataSource = _tourManager.GetAllWithName(tbxTourSearch.Text.Trim().ToString());
                        break;
                    case 1:
                        dgwTours.DataSource = _tourManager.GetAllWithType(tbxTourSearch.Text.Trim().ToString());
                        break;
                }
            }
        }
        private void LoadTourNames()
        {
            cbxAddTour.DataSource = GetTourNames();
        }
        private List<string> GetTourNames()
        {
            int sayi = _tourManager.GetAll().Count();
            List<string> arr = new List<string>(sayi);

            foreach (Tour item in _tourManager.GetAll())
            {
                arr.Add(item.Name);
            }
            return arr;
        }

        private void btnTourAdd_Click(object sender, EventArgs e)
        {
            try
            {         
                string VisaString = "";
                if (rbNo.Checked)
                {
                    VisaString = "No";
                }
                if (rbYes.Checked)
                {
                    VisaString = "Yes";
                }
                Tour tur = new Tour()
                {
                    Name = tbxTourName.Text.Trim(),
                    Price = Convert.ToDecimal(tbxTourPrice.Text),
                    Description = tbxDesc.Text.Trim(),
                    TourType = tbxTourType.Text.Trim(),
                    isVisaRequired = VisaString.Trim(),
                    isAnyMealsIncluded = cbxTourMeal.Text.Trim().ToString(),
                    StartDate = dtpStartDate.Value,
                    EndDate = dtpEndDate.Value
                };
                tbxDesc.Text = "";
                tbxTourName.Text = "";
                tbxTourPrice.Text = "";
                tbxTourType.Text = "";

                _tourManager.Add(tur);
                LoadDatas();
                ClearTourSpecs();
                MessageBox.Show("Tur Eklendi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void btnTourUpdate_Click(object sender, EventArgs e)
        {
            try
            {   string VisaString = "";
                if (rbNo.Checked)
                {
                    VisaString = "No";
                }

                if (rbYes.Checked)
                {
                    VisaString = "Yes";
                }
                int tourId = _tourManager.GetWithId(Convert.ToInt32(dgwTours.CurrentRow.Cells[0].Value)).Id;
                Tour tur = new Tour()
                {
                    Id = tourId,
                    Name = tbxTourName.Text.Trim(),
                    Price = Convert.ToDecimal(tbxTourPrice.Text),
                    Description = tbxDesc.Text.Trim(),
                    TourType = tbxTourType.Text.Trim(),
                    isVisaRequired = VisaString.Trim(),
                    isAnyMealsIncluded = cbxTourMeal.Text.Trim().ToString(),
                    StartDate = dtpStartDate.Value,
                    EndDate = dtpEndDate.Value
                };
                _tourManager.Update(tur);
                LoadDatas();
                ClearTourSpecs();
                tabControlMain.SelectedTab = tbpTurlar;
                MessageBox.Show("Başarıyla Güncellendi!");
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadTours()
        {
            dgwTours.DataSource = _tourManager.GetAll();
        }
        #endregion
        #region Customers
        private void CustomerClear()
        {
            dtpCusBirthDate.Value = DateTime.Now;
            tbxCusFirstName.Text = "";
            tbxCusLastName.Text = "";
            tbxCusMail.Text = "";
            tbxNationality.Text = "";
            tbxCusPhoneNumber.Text = "";
            cbxAddTour.Text = "";
        }
        private void btnCustomerAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Customer customer = new Customer()
                {
                    NationalityId = tbxNationality.Text.Trim(),
                    FirstName = tbxCusFirstName.Text.Trim().ToUpper(),
                    LastName = tbxCusLastName.Text.Trim().ToUpper(),
                    Birthday = Convert.ToDateTime(dtpCusBirthDate.Value),
                    Mail = tbxCusMail.Text.Trim(),
                    PhoneNumber = tbxCusPhoneNumber.Text.Trim(),
                    RegisteredTour = FindTour(cbxAddTour.Text).Name
                };
                _customerManager.Add(customer);
                CustomerClear();
                MessageBox.Show("Kullanıcı Eklendi!");
                
                
                MailTransactions.SendTourRegisterMail(customer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }
        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                int id = _customerManager.GetWithId(Convert.ToInt32(dgwCustomers.CurrentRow.Cells[0].Value)).Id;
                Customer customer = new Customer()
                {
                    Id = id,
                    NationalityId = tbxNationality.Text.Trim(),
                    FirstName = tbxCusFirstName.Text.Trim().ToUpper(),
                    LastName = tbxCusLastName.Text.Trim().ToUpper(),
                    Birthday = Convert.ToDateTime(dtpCusBirthDate.Value),
                    Mail = tbxCusMail.Text.Trim(),
                    PhoneNumber = tbxCusPhoneNumber.Text.Trim(),
                    RegisteredTour = FindTour(cbxAddTour.Text).Name
                };
                _customerManager.Update(customer);
                CustomerClear();
                tabControlMain.SelectedIndex = 2;
                MessageBox.Show("Kullanıcı Güncellendi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            LoadDatas();
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            Customer customer = _customerManager.GetWithId(Convert.ToInt32(dgwCustomers.CurrentRow.Cells[0].Value));
            _customerManager.Delete(customer);

            MessageBox.Show(customer.FirstName + " " + customer.LastName + " adlı müşterinin kaydı silinmiştir.");
            LoadDatas();
        }

        private void dgwCustomers_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Kullanıcı Bilgilerini Tuttuk.
            Customer customer = new Customer() 
            {
                Id = Convert.ToInt32(dgwCustomers.CurrentRow.Cells[0].Value), 
                NationalityId = dgwCustomers.CurrentRow.Cells[1].Value.ToString(),
                FirstName = dgwCustomers.CurrentRow.Cells[2].Value.ToString(),
                LastName = dgwCustomers.CurrentRow.Cells[3].Value.ToString(),
                Mail = dgwCustomers.CurrentRow.Cells[4].Value.ToString(),
                PhoneNumber = dgwCustomers.CurrentRow.Cells[5].Value.ToString(),
                Birthday = Convert.ToDateTime(dgwCustomers.CurrentRow.Cells[6].Value),
                RegisteredTour = dgwCustomers.CurrentRow.Cells[7].Value.ToString()
            };
            #region AddDatas
            tbxCusFirstName.Text = customer.FirstName;
            tbxCusLastName.Text = customer.LastName;
            tbxCusMail.Text = customer.Mail;
            tbxCusPhoneNumber.Text = customer.PhoneNumber;
            tbxNationality.Text = customer.NationalityId;
            dtpCusBirthDate.Value = customer.Birthday;
            cbxAddTour.Text = customer.RegisteredTour;
            tabControlMain.SelectedIndex = 3;
            #endregion

        }
        private void LoadCustomers()
        {
            dgwCustomers.DataSource = _customerManager.GetAll();
        }

        private void btnCustomerClear_Click(object sender, EventArgs e)
        {
            CustomerClear();
        }

        private void tbxCustomerSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbxCustomerSearch.Text == "")
            {
                LoadCustomers();
            }
            else
            {
                switch (cbxCustomerSearch.SelectedIndex)
                {
                    case 0:
                        // Kimlik numarası
                        dgwCustomers.DataSource = _customerManager.GetAllWithNationality(tbxCustomerSearch.Text.Trim());
                        break;
                    case 1:
                        // tur ismine göre
                        dgwCustomers.DataSource = _customerManager.GetAllWithTourName(tbxCustomerSearch.Text.Trim());
                        break;
                }
            }
        }



        #endregion
        #region Losses

        private void btnAddLoss_Click(object sender, EventArgs e)
        {
            try
            {
            Loss zarar = new Loss() 
            { 
                Name = tbxLossName.Text,
                Description = tbxLossDescription.Text,
                AmountOfLoss = Convert.ToDecimal(tbxLossAmountOfLoss.Text),
                Tour = cbxLossTours.Text
            };
                _lossManager.Add(zarar);
                LoadDatas();
                WriteAChart();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadLossesTours()
        {
            cbxLossTours.DataSource = GetTourNames();
        }
        private void LoadLosses()
        {
            LoadLossesTours();
            dgwLoss.DataSource = _lossManager.GetAll();
        }



        #endregion
        private void ControlSeries(List<string> tours)
        {
            foreach (string item in tours)
            {
                if (chartKarZarar.Series.IsUniqueName(item))
                {
                    chartKarZarar.Series.Add(item);
                }
            }
        }
        private void WriteAChart()
        {
            chartKarZarar.Series.Clear();
            List<string> tours = GetTourNames();
            ControlSeries(tours);



            foreach (string item in tours)
            {
                var losses = _lossManager.getAllWithTourName(item);
                int total = 0;
                foreach (Loss i in losses)
                {
                    total += Convert.ToInt32(i.AmountOfLoss);
                }
                chartKarZarar.Series[item].Points.Add(total);
            }
        }
        private void CalculateAllLosses()
        {
            decimal total = 0;
            foreach(var item in _lossManager.GetAll())
            {
                total += item.AmountOfLoss;
            }
            lblAllLosses.Text = total.ToString();

            decimal totalKar = 0;
            foreach(var item in _customerManager.GetAll())
            {
                totalKar += FindTour(item.RegisteredTour).Price;
            }
            lblAllTourPrices.Text = totalKar.ToString();

            lblKar.Text = Convert.ToString(Convert.ToInt32(totalKar) - Convert.ToInt32(total));
        }
        private void btnCalculateLosses_Click(object sender, EventArgs e)
        {
            if (cbxLossFilter.SelectedIndex == 0)
            {
                decimal totalLoss = 0;
                _lossManager.GetAll();
                foreach (Loss item in _lossManager.GetAll())
                {
                    totalLoss += Convert.ToDecimal(item.AmountOfLoss);
                }
                lblAllLosses.Text = totalLoss.ToString();
            }
            CalculateAllLosses();
            // tablo çizdiriyor.
            WriteAChart();
        }
        private void LoadCbxLossesFilter()
        {
            cbxLossFilter.DataSource = GetTourNames();
        }

        private void btnLossDelete_Click(object sender, EventArgs e)
        {
            var loss = _lossManager.GetWithId(Convert.ToInt32(dgwLoss.CurrentRow.Cells[0].Value));
            _lossManager.Delete(loss);
            MessageBox.Show(dgwLoss.CurrentRow.Cells[1].Value.ToString()+" isimli gider silindi!");
            LoadDatas();
            CalculateAllLosses();
            WriteAChart();
        }

        private void cbxLossFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgwLoss.DataSource = _lossManager.getAllWithTourName(cbxLossFilter.Text);
        }
    }
}
