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
using System.Collections.ObjectModel;

namespace BlockChainDataTemplates
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public Blockchain TheBlocks { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            TheBlocks = new Blockchain() { Blocks = Blockchain.Example };
            DataContext = TheBlocks;
        }

    }

    public class Block
    {
        public string ID { get; set; }
        public string Nonce { get; set; }
        public string Data { get; set; }
        public string PreviousHash { get; set; }
        public string CurrentHash { get; set; }
    }

    public class Blockchain
    {
        public ObservableCollection<Block> Blocks { get; set; }

        public static ObservableCollection<Block> Example;
        public Blockchain() //Initialisation done here 
        {
            Example = new ObservableCollection<Block>();
            Example.Add(new Block() { ID = "ID1", Nonce = "0", Data = "", PreviousHash = "", CurrentHash = "" });
            Example.Add(new Block() { ID = "ID2", Nonce = "0", Data = "", PreviousHash = "", CurrentHash = "" });
            Example.Add(new Block() { ID = "ID3", Nonce = "0", Data = "", PreviousHash = "", CurrentHash = "" });
        }

    }
}
