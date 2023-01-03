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
using Microsoft.VisualBasic;
using Turizm.Utils.Abstract;
using Turizm.Utils.Concrete;

namespace Turizm.UI
{
    public partial class Form1 : Form
    {
        private AccountManager _accountManager;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _accountManager = new AccountManager(new EfAccountDal());
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {

            bool result = _accountManager.LogIn(new Account()
            {
                Mail = tbxMail.Text.ToString(),
                Password = tbxPassword.Text.ToString()
            });
            if (result)
            {
                MainMenu m = new MainMenu();
                m.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Geçersiz mail veya geçersiz şifre.Lütfen tekrar deneyiniz.");
            }
        }
    }
}
