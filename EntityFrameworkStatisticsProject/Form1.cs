using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkStatisticsProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string connectionString = ConnectionStringHelper.GetConnectionString("DbStatisticsEntities");
        DbStatisticsEntities db = new DbStatisticsEntities(connectionString);

        private void Form1_Load(object sender, EventArgs e)
        {
            // Toplam Kategori Sayısı
            int categoryCount = db.TblCategory.Count();
            lblCategoryCount.Text = categoryCount.ToString();

            // Toplam Ürün Sayısı
            int productCount = db.TblProduct.Count();
            lblProductCount.Text = productCount.ToString();

            // Toplam Müşteri Sayısı
            int customerCount = db.TblCustomer.Count();
            lblCustomerCount.Text = customerCount.ToString();

            // Toplam Sipariş Sayısı
            int orderCount = db.TblOrder.Count();
            lblOrderCount.Text = orderCount.ToString();

            // Toplam Stok Sayısı
            // Sum method sums all the data in a column in Entity Framework
            var totalProductStockCount = db.TblProduct.Sum(x => x.ProductStock);
            lblProductTotalStock.Text = totalProductStockCount.ToString();

            // Ortalama Ürün Fiyatı
            var averageProductPrice = db.TblProduct.Average(x => x.ProductPrice);
            lblProductAveragePrice.Text = averageProductPrice.ToString() + " ₺";

            // Toplam Meyve Stoğu
            var totalFruitCount = db.TblProduct.Where(x => x.CategoryId == 1).Sum(y => y.ProductStock);
            lblTotalFruitCount.Text = totalFruitCount.ToString();

            // Gazoz Toplam İşlem Hacmi
            var totalStockOfGazoz = db.TblProduct.Where(x => x.ProductName == "Gazoz").Select(y => y.ProductStock).FirstOrDefault();
            var totalPriceOfGazoz = db.TblProduct.Where(x => x.ProductName == "Gazoz").Select(y => y.ProductPrice).FirstOrDefault();
            var totalVolume = totalStockOfGazoz * totalPriceOfGazoz;
            lblTotalPriceOfGazoz.Text = totalVolume.ToString() + " ₺";

            // Stok Sayısı 100'den Az Olan Ürün Sayısı
            var productStockLess100 = db.TblProduct.Where(x => x.ProductStock < 100).Count();
            lblProductStockLess100.Text = productStockLess100.ToString();

            // Aktif Sebze Stoğu

            // Query could be done with CategoryId
            //var activeVegetableStock = db.TblProduct.Where(x => x.CategoryId == 2 && x.ProductStatus == true).Sum(y => y.ProductStock);
            
            // But if we want to write it with CategoryName, it should be like this (We used sub query):
            var activeVegetableStock = db.TblProduct.Where(x => x.CategoryId == (db.TblCategory.Where(z => z.CategoryName == "Sebze").Select(w => w.CategoryId).FirstOrDefault()) && x.ProductStatus == true).Sum(y => y.ProductStock);
            lblActiveVegetableStock.Text = activeVegetableStock.ToString();

            // Türkiye'den Yapılan Sipariş Sayısı

        }

        private void lblOrderCount_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
