using McDonaldsCoreApp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;


namespace McDonaldsKitchen
{
    public partial class Kitchen : Form
    {
        List<Order> _orders = new List<Order>();
        public Kitchen()
        {
            InitializeComponent();
            // Daha fazla sipariş ekleyebilirsiniz...
            this.MinimumSize = new Size(290, this.MinimumSize.Height);
        }

        private void Kitchen_Load(object sender, EventArgs e)
        {
            InitializeMocks();
            DisplayOrders();
            TcpJsonGonder();

            JsonVeriDinle();
        }

        public void InitializeMocks()
        {
            Product product1 = new Product { Id = 1, Name = "Big Mac", Quantity = 2, OrderId = 1 };
            Product product2 = new Product { Id = 2, Name = "Kola", Quantity = 1, OrderId = 1 };
            Product product3 = new Product { Id = 3, Name = "Patates Kızartması", Quantity = 1, OrderId = 1 };
            Product product4 = new Product { Id = 4, Name = "McChicken", Quantity = 1, OrderId = 2 };
            Product product5 = new Product { Id = 5, Name = "Fanta", Quantity = 2, OrderId = 2 };

            Order order1 = new Order
            {
                Id = 1,
                OrderStatus = "hazırlanıyor",
                Products = new List<Product> { product1, product2, product3, product1, product2, product3, product1, product2, product3, product1, product2, product3 }
            };

            // İkinci sipariş örneği
            Order order2 = new Order
            {
                Id = 2,
                OrderStatus = "hazırlanıyor",
                Products = new List<Product> { product4, product5 }
            };
            Order order3 = new Order
            {
                Id = 3,
                OrderStatus = "hazırlanıyor",
                Products = new List<Product> { product1, product2, product3, product1, product2, product3, product1, product2, product3, product1, product2, product3 }
            };

            Order order4 = new Order
            {
                Id = 4,
                OrderStatus = "hazırlanıyor",
                Products = new List<Product> { product4, product5 }
            };

            Order order5 = new Order
            {
                Id = 5,
                OrderStatus = "hazırlanıyor",
                Products = new List<Product> { product1, product2, product3, product1, product2, product3, product1, product2, product3, product1, product2, product3 }
            };
            // İkinci sipariş örneği
            Order order6 = new Order
            {
                Id = 6,
                OrderStatus = "hazırlanıyor",
                Products = new List<Product> { product4, product5 }
            };
            Order order7 = new Order
            {
                Id = 7,
                OrderStatus = "hazırlanıyor",
                Products = new List<Product> { product1, product2, product3, product1, product2, product3, product1, product2, product3, product1, product2, product3 }
            };

            Order order8 = new Order
            {
                Id = 8,
                OrderStatus = "hazır",
                Products = new List<Product> { product4, product5 }
            };

            _orders.Add(order1);
            _orders.Add(order2);
            _orders.Add(order3);
            _orders.Add(order4);
            _orders.Add(order5);
            _orders.Add(order6);
            _orders.Add(order7);
            _orders.Add(order8);
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
                        order.OrderStatus = "hazır"; // Order'ın durumunu güncelle
                        
                        Console.Beep();
                    }
                    else if (order.OrderStatus.ToLower() == "hazır")
                    {
                        groupBox.BackColor = Color.White;
                        order.OrderStatus = "teslim"; // Order'ın durumunu güncelle
                        Console.Beep();
                        _orders.Remove(order);
                    }
                    //

                }
            }

            DisplayOrders();
            TcpJsonGonder();
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


            for (int i = 0; i < _orders.Count; i++)
            {



                GroupBox groupBox = CreateGroupBox(_orders[i], groupBoxWidth, groupBoxHeight);
                groupBox.Tag = _orders[i]; // GroupBox ile ilişkili Order nesnesini sakla

                // GroupBox tıklama olayını ekle
                groupBox.Click += GroupBox_Click;

                Panel scrollablePanel = CreateScrollablePanel(groupBoxWidth, (groupBoxHeight - 100));
                groupBox.Controls.Add(scrollablePanel);


                //groupBox.Controls.Add(scrollablePanel);




                foreach (Product product in _orders[i].Products)
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

        public void JsonVeriDinle(string IpAddress = "192.168.88.1", int port = 1453)
        {
            IPAddress localAddr = IPAddress.Parse(IpAddress);

            // 
            TcpListener server = new TcpListener(localAddr, port);
            server.Start();

            Task.Run(() => // Asenkron iş parçacığında server dinlemeye başlar
            {
                while (true)
                {
                    using (TcpClient client = server.AcceptTcpClient())
                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] lengthBytes = new byte[4];
                        stream.Read(lengthBytes, 0, 4);
                        int length = BitConverter.ToInt32(lengthBytes, 0);

                        byte[] jsonBytes = new byte[length];
                        stream.Read(jsonBytes, 0, jsonBytes.Length);

                        string jsonString = Encoding.UTF8.GetString(jsonBytes);

                        List<Order> receivedOrders = JsonSerializer.Deserialize<List<Order>>(jsonString);

                        // UI güncellemeleri UI thread'inde yapılmalı
                        this.Invoke((MethodInvoker)delegate
                        {
                            _orders.Clear();
                            foreach (Order order in receivedOrders)
                            {
                                if (order.OrderStatus.ToLower() != "teslim")
                                {
                                    _orders.Add(order);
                                    DisplayOrders(); // UI güncellemesi
                                }
                            }


                        });
                    }
                }
            });


        }

        public void TcpJsonGonder(string IpAdress= "192.168.88.1", int serverPort = 1071)
        {
            IPAddress serverAddr = IPAddress.Parse(IpAdress);
            

            // İstemci
            Task.Run(() =>
            {
                
                    using (TcpClient client = new TcpClient(serverAddr.ToString(), serverPort))
                    using (NetworkStream stream = client.GetStream())
                    {
                        // List<Order> nesnesini JSON'a dönüştür
                        string json = JsonSerializer.Serialize(_orders);

                        // JSON string'ini byte dizisine dönüştür
                        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

                        // Byte dizisinin uzunluğunu gönder (opsiyonel, ancak alıcı tarafın ne kadar veri okuyacağını bilmek için yararlı olabilir)
                        byte[] lengthPrefix = BitConverter.GetBytes(jsonBytes.Length);
                        stream.Write(lengthPrefix, 0, lengthPrefix.Length);

                        // JSON verisini gönder
                        stream.Write(jsonBytes, 0, jsonBytes.Length);
                    }
                
            });
            
        }

    }
}
