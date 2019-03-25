using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainDataTemplates
{
    public class Block : INotifyPropertyChanged
    {
        private const string SignKey = "0000";

        private string id;
        
        public string ID { get => id; set { id = value; NotifyPropertyChanged(); } }

        private string nonce;
     
        public string Nonce { get => nonce; set { nonce = value; NotifyPropertyChanged(); } }

        private string data;
     
        public string Data { get => data; set { data = value; NotifyPropertyChanged(); } }

        private string previoushash;
        
        public string PreviousHash { get => previoushash; set { previoushash = value; NotifyPropertyChanged(); } }

        private string currenthash;
       
        public string CurrentHash { get => currenthash; set { currenthash = value; NotifyPropertyChanged(); } }

        /// <summary>
        /// Sets Default values for each block
        /// </summary>
        public Block (string id)
        {

            this.id = id;
            Nonce = "0";
            Data = string.Empty;
            PreviousHash = "00000000000";
            PropertyChanged += InternalHandler;
            
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSigned => string.Equals(CurrentHash.Substring(0,SignKey.Length), SignKey);

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InternalHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentHash" || e.PropertyName == "IsSigned") return;
            this.Rehash();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Rehash()
        {
            CurrentHash = HashGenerator.HashGen(ID, Nonce, Data, PreviousHash);
            PropertyChanged(this, new PropertyChangedEventArgs("IsSigned"));
        }

        /// <summary>
        /// Public static bool to get or privately set "IsMining" function as false
        /// </summary>
        public static bool IsMining { get; private set; } = false;

        /// <summary>
        /// Public void to return the "IsMining" function if "true"
        /// </summary>
        public void Mine()
        {
            if (IsMining) return;
            IsMining = true;
            Nonce = "0";
            while (!IsSigned)
                Nonce = (int.Parse(Nonce) + 1).ToString();
            IsMining = false;
            NotifyPropertyChanged("CurrentHash");
        }
    }
    /// <summary>
    /// BlockChain Class
    /// </summary>
    public class BlockChain
    {
        public ObservableCollection<Block> Blocks { get; } = new ObservableCollection<Block>();

        /// <summary>
        /// Adds a Block into the BlockChain
        /// </summary>
        /// <param name="B"></param>
        public void Add(Block B)
        {
            B.PreviousHash = B.ID.Equals("0") ? "0000000000000000000000000000000000000000" : Blocks[Blocks.Count - 1].CurrentHash;
            Blocks.Add(B);
            B.PropertyChanged += InternalHandler;
        }

        private void InternalHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "CurrentHash") return;
            if (sender is Block B)
            {
                if (Block.IsMining) return;
                int index = int.Parse(B.ID);
                if (index + 1 == Blocks.Count) return;                        // Last Block has nothing to do
                Blocks[index + 1].PreviousHash = Blocks[index].CurrentHash;
            }
        }
        public static BlockChain GetInitialisedBlockChain()
        {
            BlockChain result = new BlockChain();
            foreach (int I in Enumerable.Range(0, 5))
                result.Add(new Block(I.ToString()));
            return result;
        }
    }
        
    
}
