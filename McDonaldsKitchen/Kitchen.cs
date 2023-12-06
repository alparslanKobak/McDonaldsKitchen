using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace McDonaldsKitchen
{
    public partial class Kitchen : Form
    {


        List<Order> orders = new List<Order>();
        public Kitchen()
        {
            InitializeComponent();

            // Daha fazla sipariş ekleyebilirsiniz...
            this.MinimumSize = new Size(290, this.MinimumSize.Height);

        }

        private void Kitchen_Load(object sender, EventArgs e)
        {
            orders.Add(new Order() { Id = 1, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 2, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 3, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 4, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 5, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 6, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });

            orders.Add(new Order() { Id = 1, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 2, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 3, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 4, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 5, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 6, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });

            orders.Add(new Order() { Id = 1, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 2, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 3, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 4, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 5, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 6, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });

            orders.Add(new Order() { Id = 1, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 2, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 3, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 4, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 5, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });
            orders.Add(new Order() { Id = 6, OrderName = "Big Mac", OrderPrice = "$5", OrderStatus = "Hazırlanıyor" });

            


            DisplayOrders();
        }

        private void DisplayOrders()
        {
            mainPanel.Controls.Clear(); // Mevcut kontrolleri temizle

            // GroupBox ölçülerini ve aralıkları tanımla
            int groupBoxWidth = 200; // GroupBox genişliği
            int groupBoxHeight = 250; // GroupBox yüksekliği
            int horizontalSpacing = 30; // Yatay aralığı genişlet
            int verticalSpacing = 30; // Dikey aralığı genişlet
            int marginTop = 40; // İlk satır için üst boşluk
            int margin = 20; // İlk GroupBox için sol boşluk

            // Maksimum GroupBox sayısını pencere genişliğine göre belirle
            int groupBoxesPerRow;
            if (mainPanel.Width < this.MinimumSize.Width)
            {
                groupBoxesPerRow = 1;
            }
            else
            {
                groupBoxesPerRow = this.WindowState == FormWindowState.Maximized ? 8 : (mainPanel.Width - margin) / (groupBoxWidth + horizontalSpacing);
                if (groupBoxesPerRow == 0) groupBoxesPerRow = 1; // Sıfırdan kaçınmak için kontrol et
            }

            for (int i = 0; i < orders.Count; i++)
            {
                // GroupBox oluştur ve özelliklerini ayarla
                GroupBox groupBox = new GroupBox
                {
                    Text = orders[i].OrderName,
                    Size = new Size(groupBoxWidth, groupBoxHeight),
                    Font = new Font("Microsoft Tai Le", 14),
                    BackColor = Color.White,
                    AccessibilityObject = { Name = orders[i].Id.ToString() }
                };

                // GroupBox konumunu hesapla
                int row = i / groupBoxesPerRow; // Her satır için GroupBox sayısını hesapla
                int column = i % groupBoxesPerRow; // Her satırda kaçıncı GroupBox olduğunu hesapla
                int x = column * (groupBoxWidth + horizontalSpacing) + margin; 
                int y = row * (groupBoxHeight + verticalSpacing) + marginTop; 

                // Her satırın ilk GroupBox'ı için sol boşluğu ayarla
                if (column == 0) x = margin;

                groupBox.Location = new Point(x, y);

                // Sipariş detayları için Label'lar ekle
                Label priceLabel = new Label
                {
                    Text = "Price: " + orders[i].OrderPrice,
                    Location = new Point(10, 20),
                    Size = new Size(groupBoxWidth - 20, 20)
                };

                Label statusLabel = new Label
                {
                    Text = "Status: " + orders[i].OrderStatus,
                    Location = new Point(10, 40),
                    Size = new Size(groupBoxWidth - 20, 20)
                };

                // Label'ları GroupBox'a ekle
                groupBox.Controls.Add(priceLabel);
                groupBox.Controls.Add(statusLabel);

                // GroupBox'ı mainPanel'e ekle
                mainPanel.Controls.Add(groupBox);
            }
        }

        private void Kitchen_SizeChanged(object sender, EventArgs e)
        {
            // headerPanel'in genişliğinin ortasını hesapla, ardından titleLabel'ın yarısını çıkararak titleLabel'ı ortala
            int formThisMiddleWidth = (headerPanel.Width / 2) - (titleLabel.Width / 2);

            // headerPanel'in yüksekliğinin ortasını hesapla, ardından titleLabel'ın yarısını çıkararak titleLabel'ı dikey olarak ortala
            int formHeight = (headerPanel.Height / 2) - (titleLabel.Height / 2);

            // titleLabel'ın yeni konumunu ayarla
            titleLabel.Location = new Point(formThisMiddleWidth, formHeight);
            DisplayOrders();
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public class Order
    {
        public int Id { get; set; }
        public string OrderName { get; set; }
        public string OrderPrice { get; set; }
        public string OrderStatus { get; set; }
    }
}
