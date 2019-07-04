using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsProject
{
    public class HuffmanNode
    {
        private string _binary_word;
        private bool _is_left_node;
        private bool _is_right_node;
        private HuffmanNode _left_node;
        private HuffmanNode _parent_node;
        private HuffmanNode _right_node;
        private char? _value;
        private int _weight;

        public HuffmanNode Left
        {
            get
            {
                return _left_node;
            }
        }

        public HuffmanNode Parent
        {
            get
            {
                return _parent_node;
            }
        }

        public HuffmanNode Right
        {
            get
            {
                return _right_node;
            }
        }

        public char? Value
        {
            get
            {
                return _value;
            }
        }

        public int Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
            }
        }

        public HuffmanNode(char value)
        {
            _value = value;
        }

        public HuffmanNode(HuffmanNode left_node, HuffmanNode right_node)
        {
            _left_node = left_node;
            _left_node._parent_node = this;
            _left_node._is_left_node = true;

            _right_node = right_node;
            _right_node._parent_node = this;
            _right_node._is_right_node = true;

            _weight = (_left_node.Weight + _right_node.Weight);
        }

        public string Binary_Word
        {
            get
            {
                string return_value = "";

                if (String.IsNullOrEmpty(_binary_word))
                {
                    StringBuilder string_builder = new StringBuilder();

                    HuffmanNode huffman_node = this;

                    while (huffman_node != null)
                    {
                        if (huffman_node._is_left_node)
                        {
                            string_builder.Insert(0, "0");
                        }

                        if (huffman_node._is_right_node)
                        {
                            string_builder.Insert(0, "1");
                        }

                        huffman_node = huffman_node._parent_node;
                    }

                    return_value = string_builder.ToString();
                    _binary_word = return_value;
                }
                else
                {
                    return_value = _binary_word;
                }

                return return_value;
            }
        }

        public override string ToString()
        {
            StringBuilder string_builder = new StringBuilder();

            if (_value.HasValue)
            {
                string_builder.Append($"'{_value.Value}' ({_weight}) - {Binary_Word} ({Binary_Word.BinaryStringToInt32()})");
            }
            else
            {
                if ((_left_node != null) && (_right_node != null))
                {
                    if ((_left_node.Value.HasValue) && (_right_node.Value.HasValue))
                    {
                        string_builder.Append($"{_left_node.Value} + {_right_node.Value} ({_weight})");
                    }
                    else
                    {
                        string_builder.Append($"{_left_node}, {_right_node} - ({_weight})");
                    }
                }
                else
                {
                    string_builder.Append(_weight);
                }
            }

            return string_builder.ToString();
        }
    }
}