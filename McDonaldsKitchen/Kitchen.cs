using McDonaldsCoreApp;
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
            Product product1 = new Product { Id = 1, Name = "Big Mac", Quantity = 2, OrderId = 1 };
            Product product2 = new Product { Id = 2, Name = "Kola", Quantity = 1, OrderId = 1 };
            Product product3 = new Product { Id = 3, Name = "Patates Kızartması", Quantity = 1, OrderId = 1 };
            Product product4 = new Product { Id = 4, Name = "McChicken", Quantity = 1, OrderId = 2 };
            Product product5 = new Product { Id = 5, Name = "Fanta", Quantity = 2, OrderId = 2 };

            Order order1 = new Order
            {
                Id = 1,
                OrderStatus = "Hazırlanıyor",
                Products = new List<Product> { product1, product2, product3, product1, product2, product3, product1, product2, product3, product1, product2, product3 }
            };

            // İkinci sipariş örneği
            Order order2 = new Order
            {
                Id = 2,
                OrderStatus = "Hazırlanıyor",
                Products = new List<Product> { product4, product5 }
            };
            Order order3 = new Order
            {
                Id = 3,
                OrderStatus = "Hazırlanıyor",
                Products = new List<Product> { product1, product2, product3, product1, product2, product3, product1, product2, product3, product1, product2, product3 }
            };

            Order order4 = new Order
            {
                Id = 4,
                OrderStatus = "Hazırlanıyor",
                Products = new List<Product> { product4, product5 }
            };

            Order order5 = new Order
            {
                Id = 5,
                OrderStatus = "Hazırlanıyor",
                Products = new List<Product> { product1, product2, product3, product1, product2, product3, product1, product2, product3, product1, product2, product3 }
            };
            // İkinci sipariş örneği
            Order order6 = new Order
            {
                Id = 6,
                OrderStatus = "Hazırlanıyor",
                Products = new List<Product> { product4, product5 }
            };
            Order order7 = new Order
            {
                Id = 7,
                OrderStatus = "Hazırlanıyor",
                Products = new List<Product> { product1, product2, product3, product1, product2, product3, product1, product2, product3, product1, product2, product3 }
            };

            Order order8 = new Order
            {
                Id = 8,
                OrderStatus = "Hazırlanıyor",
                Products = new List<Product> { product4, product5 }
            };

            orders.Add(order1);
            orders.Add(order2);
            orders.Add(order3);
            orders.Add(order4);
            orders.Add(order5);
            orders.Add(order6);
            orders.Add(order7);
            orders.Add(order8);






            DisplayOrders();
        }
        private void GroupBox_Click(object sender, EventArgs e)
        {
            GroupBox groupBox = sender as GroupBox;
            if (groupBox != null)
            {
                Order order = groupBox.Tag as Order;
                if (order != null)
                {
                    if (order.OrderStatus.ToLower() == "hazırlanıyor")
                    {
                        groupBox.BackColor = Color.Green;
                        order.OrderStatus = "Hazır"; // Order'ın durumunu güncelle
                        Console.Beep();
                    }
                    else if (order.OrderStatus.ToLower() == "hazır")
                    {
                        groupBox.BackColor = Color.White;
                        order.OrderStatus = "teslim"; // Order'ın durumunu güncelle
                        Console.Beep();
                        orders.Remove(order);
                    }

                }
            }

            DisplayOrders();
        }


        private void DisplayOrders()
        {
            mainPanel.Controls.Clear(); // Mevcut kontrolleri temizle

            int groupBoxWidth = 300;
            int groupBoxHeight = 250;
            int horizontalSpacing = 30;
            int verticalSpacing = 30;
            int marginTop = 40;
            int marginLeft = 20;

            int groupBoxesPerRow = CalculateGroupBoxesPerRow(mainPanel.Width, groupBoxWidth, horizontalSpacing, marginLeft);


            for (int i = 0; i < orders.Count; i++)
            {



                GroupBox groupBox = CreateGroupBox(orders[i], groupBoxWidth, groupBoxHeight);
                groupBox.Tag = orders[i]; // GroupBox ile ilişkili Order nesnesini sakla

                // GroupBox tıklama olayını ekle
                groupBox.Click += GroupBox_Click;

                Panel scrollablePanel = CreateScrollablePanel(groupBoxWidth, (groupBoxHeight - 100));
                groupBox.Controls.Add(scrollablePanel);


                //groupBox.Controls.Add(scrollablePanel);




                foreach (Product product in orders[i].Products)
                {
                    AddProductToPanel(scrollablePanel, product, groupBoxWidth);
                }

                PositionGroupBox(groupBox, i, groupBoxesPerRow, groupBoxWidth, groupBoxHeight, horizontalSpacing, verticalSpacing, marginTop, marginLeft);
                mainPanel.Controls.Add(groupBox);

            }
        }


        private int CalculateGroupBoxesPerRow(int panelWidth, int groupBoxWidth, int spacing, int marginLeft)
        {
            // Eğer pencere genişliği minimum genişlikten küçükse, bir satıra bir GroupBox sığdır
            int widthAvailable = panelWidth - marginLeft; // Sol boşluk düşüldükten sonra kullanılabilir genişlik
            int groupBoxesPerRow = widthAvailable / (groupBoxWidth + spacing);
            return groupBoxesPerRow > 0 ? groupBoxesPerRow : 1;
        }

        private GroupBox CreateGroupBox(Order order, int width, int height)
        {
            return new GroupBox
            {
                Text = "Sipariş NO: " + order.Id + " || " + order.OrderStatus,
                Size = new Size(width, height),
                Font = new Font("Microsoft Tai Le", 14, FontStyle.Bold),
                BackColor = order.OrderStatus.ToLower() == "hazırlanıyor" ? Color.White : Color.Green,

            };
        }

        private Panel CreateScrollablePanel(int width, int height)
        {
            return new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Width = width,
                Height = height
            };
        }



        private void AddProductToPanel(Panel panel, Product product, int panelWidth)
        {
            Label nameLabel = new Label
            {
                Text = product.Quantity.ToString() + " X " + product.Name,
                AutoSize = true,
                Location = new Point(10, panel.Controls.Count * 20)
            };



            panel.Controls.Add(nameLabel);

        }

        private void PositionGroupBox(GroupBox groupBox, int index, int groupBoxesPerRow, int groupBoxWidth, int groupBoxHeight, int horizontalSpacing, int verticalSpacing, int marginTop, int marginLeft)
        {
            int row = index / groupBoxesPerRow;
            int column = index % groupBoxesPerRow;
            groupBox.Location = new Point(
                marginLeft + (column * (groupBoxWidth + horizontalSpacing)),
                marginTop + (row * (groupBoxHeight + verticalSpacing))
            );
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


}
