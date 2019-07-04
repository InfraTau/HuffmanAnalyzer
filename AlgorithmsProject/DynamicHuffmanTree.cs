using System;
using System.Collections.Generic;

namespace AlgorithmsProject
{
    internal class DynamicHuffmanTree
    {
        private DynamicHuffmanTree Left;
        private DynamicHuffmanTree Right;
        private int Number;
        private Byte? Symbol; 
        private string SpecialSymbol;
        private int Weight;

        public void CreateModel()
        {
            Number = 3;
            Weight = 2;
            Left = new DynamicHuffmanTree();
            Left.SpecialSymbol = "Esc";
            Left.Number = 1;
            Left.Weight = 1;
            Right = new DynamicHuffmanTree();
            Right.SpecialSymbol = "EoF";
            Right.Number = 2;
            Right.Weight = 1;
        }

        #region Find
        private DynamicHuffmanTree Find(Byte? SymbolToFind)
        {
            if (Symbol == SymbolToFind) return this;
            DynamicHuffmanTree Result = null;
            if (Right != null) Result = Right.Find(SymbolToFind);
            if (Result == null && Left != null) Result = Left.Find(SymbolToFind);
            return Result;
        }

        private DynamicHuffmanTree Find(string SpecialSymbolToFind)
        {
            if (SpecialSymbol == SpecialSymbolToFind) return this;
            DynamicHuffmanTree Result = null;
            if (Right != null) Result = Right.Find(SpecialSymbolToFind);
            if (Result == null && Left != null) Result = Left.Find(SpecialSymbolToFind);
            return Result;
        }

        private DynamicHuffmanTree Find(int NumberToFind)
        {
            if (Number == NumberToFind) return this;
            DynamicHuffmanTree Result = null;
            if (Right != null) Result = Right.Find(NumberToFind);
            if (Result == null && Left != null) Result = Left.Find(NumberToFind);
            return Result;
        }

        private DynamicHuffmanTree FindParent(DynamicHuffmanTree Child)
        {
            if (Left == Child || Right == Child || this == Child) return this;
            DynamicHuffmanTree Result = null;
            if (Right != null) Result = Right.FindParent(Child);
            if (Result == null && Left != null) Result = Left.FindParent(Child);
            return Result;
        }
        #endregion

        private void Rebuild(Byte? SymbolToEncode)
        {
            DynamicHuffmanTree CurrentVertex = Find(SymbolToEncode);

            if (CurrentVertex == null)
            {
                DynamicHuffmanTree NewVertex = new DynamicHuffmanTree();
                DynamicHuffmanTree LastVertex = Find(1);
                DynamicHuffmanTree LastVertexParent = FindParent(LastVertex);
                DynamicHuffmanTree VertexWithSymbol = new DynamicHuffmanTree();
                VertexWithSymbol.Symbol = SymbolToEncode;
                VertexWithSymbol.Weight = 1;
                NewVertex.Weight = LastVertex.Weight + VertexWithSymbol.Weight;
                LastVertexParent.Left = NewVertex;
                NewVertex.Left = VertexWithSymbol;
                NewVertex.Right = LastVertex;
                CurrentVertex = NewVertex;
                ReNumber();
            }
            else CurrentVertex.Weight++;

            while (CurrentVertex != this)
            {
                int Number = CurrentVertex.Number;
                while (CurrentVertex.Weight == Find(Number + 1).Weight + 1) Number++;

                if (Number != CurrentVertex.Number)
                {
                    DynamicHuffmanTree VertexForChange;
                    VertexForChange = Find(Number);

                    if (FindParent(VertexForChange) != FindParent(CurrentVertex))
                    {
                        DynamicHuffmanTree Parent1 = FindParent(VertexForChange);
                        DynamicHuffmanTree Parent2 = FindParent(CurrentVertex);
                        if (Parent1.Left == VertexForChange) Parent1.Left = CurrentVertex;
                        if (Parent1.Right == VertexForChange) Parent1.Right = CurrentVertex;
                        if (Parent2.Left == CurrentVertex) Parent2.Left = VertexForChange;
                        if (Parent2.Right == CurrentVertex) Parent2.Right = VertexForChange;
                    }
                    else
                    {
                        DynamicHuffmanTree Parent = FindParent(VertexForChange);
                        if (Parent.Left == VertexForChange)
                        {
                            Parent.Left = CurrentVertex;
                            Parent.Right = VertexForChange;
                        }
                        if (Parent.Left == CurrentVertex)
                        {
                            Parent.Left = VertexForChange;
                            Parent.Right = CurrentVertex;
                        }
                    }
                }
                CurrentVertex = FindParent(CurrentVertex);
                CurrentVertex.Weight++;
                ReNumber();
            }
        }

        private int Count()
        {
            int Quantity = 1;
            if (Right != null) Quantity += Right.Count();
            if (Left != null) Quantity += Left.Count();
            return Quantity;
        }

        private void ReNumber()
        {
            int index = this.Count();
            DynamicHuffmanTree CurrentVertex;
            Queue<DynamicHuffmanTree> Queue = new Queue<DynamicHuffmanTree>();
            Queue.Enqueue(this);
            do
            {
                CurrentVertex = Queue.Dequeue();
                CurrentVertex.Number = index;
                index--;
                if (CurrentVertex.Right != null) Queue.Enqueue(CurrentVertex.Right);
                if (CurrentVertex.Left != null) Queue.Enqueue(CurrentVertex.Left);
            }
            while (Queue.Count != 0);
        }

        public string Encode(Byte? SymbolToEncode)
        {
            string Code = "";
            char a = Convert.ToChar(SymbolToEncode);
            DynamicHuffmanTree KeyLeave;
            if (SymbolToEncode == null) KeyLeave = Find("EoF");
            else
            {
                KeyLeave = Find(SymbolToEncode);
                if (KeyLeave == null)
                {
                    Code = DynamicHuffmanTree.ToBinaryString(SymbolToEncode);
                    KeyLeave = Find("Esc");
                }
            }
            DynamicHuffmanTree Parent = this.FindParent(KeyLeave);
            do
            {
                if (Parent.Left == KeyLeave) Code = "0" + Code;
                if (Parent.Right == KeyLeave) Code = "1" + Code;
                KeyLeave = Parent;
                Parent = this.FindParent(KeyLeave);
            }
            while (Parent != KeyLeave);

            Rebuild(SymbolToEncode);

            return Code;
        }

        public static Byte ToByte(string str)
        {
            Byte value = 0;
            for (int i = 0; i < 8; i++)
            {
                value += (byte)(byte.Parse(str.Substring(7 - i, 1)) * Math.Pow(2, i));
            }
            return value;
        }

        public static string ToBinaryString(Byte? SymbolToConvert)
        {
            string Result = "";
            while (SymbolToConvert != 0)
            {
                Result = (SymbolToConvert % 2).ToString() + Result;
                SymbolToConvert /= 2;
            }
            while (Result.Length != 8) Result = "0" + Result;
            return Result;
        }
    }
}
