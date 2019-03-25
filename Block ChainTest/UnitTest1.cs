using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlockChainDataTemplates;
using System.Linq;
using static BlockChainDataTemplates.BlockChain;
using System.Threading.Tasks;

namespace Block_ChainTest
{
    [TestClass]
    public class UnitTest1
    {
        BlockChain TestObject;

        [TestInitialize]
        public void SetBlockChainToInitialState()
        {
            TestObject = GetInitialisedBlockChain();
            string[] testData = new string[] { "x", "a", "b", "c", "d" };
            /* Set Data */
            for (int i = 0; i < 5; i++)
            {
                TestObject.Blocks[i].Data = testData[i];
            }
        }

        public void IsMined()
        {
            foreach (int i in Enumerable.Range(0, 5))
            {
                if (TestObject.Blocks[i] is Block B)
                {
                    if (!B.IsSigned)
                    {
                        Task.Factory.StartNew(() => B.Mine()).Wait();
                    }
                }
            }
        }

        [TestMethod]
        public void NonceChecker()
        {
            IsMined();
            Assert.AreEqual(TestObject.Blocks[0].Nonce, "1615");
            Assert.AreEqual(TestObject.Blocks[1].Nonce, "11008");
            Assert.AreEqual(TestObject.Blocks[2].Nonce, "32649");
            Assert.AreEqual(TestObject.Blocks[3].Nonce, "50744");
            Assert.AreEqual(TestObject.Blocks[4].Nonce, "133232");
        }
        [TestMethod]
        public void PreviousHashChecker()
        {
            IsMined();
            Assert.AreEqual(TestObject.Blocks[0].PreviousHash, "0000000000000000000000000000000000000000");
            Assert.AreEqual(TestObject.Blocks[1].PreviousHash, TestObject.Blocks[0].CurrentHash);
            Assert.AreEqual(TestObject.Blocks[2].PreviousHash, TestObject.Blocks[1].CurrentHash);
            Assert.AreEqual(TestObject.Blocks[3].PreviousHash, TestObject.Blocks[2].CurrentHash);
            Assert.AreEqual(TestObject.Blocks[4].PreviousHash, TestObject.Blocks[3].CurrentHash);
        }
        [TestMethod]
        public void IDChecker()
        {
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(TestObject.Blocks[i].ID, i.ToString());
            }
        }

        [TestMethod]
        public void SignChecker()
        {
            IsMined();
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(TestObject.Blocks[i].IsSigned, true);
            }
        }

        [TestMethod]
        public void CurrentHashChecker()
        {
            IsMined();
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(TestObject.Blocks[4].CurrentHash, "00003640e29e97612455d744bbaa9d788d1bb6bc");
            }
        }
    }
}
