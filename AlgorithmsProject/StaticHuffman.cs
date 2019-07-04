using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AlgorithmsProject
{
    public class StaticHuffman
    {
        private HuffmanNode _root_huffman_node;
        private List<HuffmanNode> _value_huffman_nodes;

        private List<HuffmanNode> Build_Binary_Tree(string value)
        {
            List<HuffmanNode> huffman_nodes = Get_Initial_Node_List();

            //Update node weights
            value.ToList().ForEach(m => huffman_nodes[m].Weight++);

            //Select only nodes that have a weight
            huffman_nodes = huffman_nodes
                .Where(m => (m.Weight > 0))
                .OrderBy(m => (m.Weight))
                .ThenBy(m => (m.Value))
                .ToList();

            //Assign parent nodes
            huffman_nodes = Update_Node_Parents(huffman_nodes);

            _root_huffman_node = huffman_nodes[0];
            huffman_nodes.Clear();

            //Sort nodes into a tree
            Sort_Nodes(_root_huffman_node, huffman_nodes);

            return huffman_nodes;
        }

        public double Compress(string file_name)
        {
            FileInfo file_info = new FileInfo(file_name);

            if (file_info.Exists)
            {
                string file_contents = "";

                using (StreamReader stream_reader = new StreamReader(File.OpenRead(file_name)))
                    file_contents = stream_reader.ReadToEnd();

                List<HuffmanNode> huffman_nodes = Build_Binary_Tree(file_contents);

                //Filter to nodes we care about
                _value_huffman_nodes = huffman_nodes
                    .Where(m => (m.Value.HasValue))
                    .OrderBy(m => (m.Binary_Word))
                    .ToList();

                //Construct char to binary word dictionary for quick value to binary word resolution
                Dictionary<char, string> char_to_binary_word_dictionary = new Dictionary<char, string>();

                foreach (HuffmanNode huffman_node in _value_huffman_nodes)
                    char_to_binary_word_dictionary.Add(huffman_node.Value.Value, huffman_node.Binary_Word);

                StringBuilder string_builder = new StringBuilder();
                List<byte> byte_list = new List<byte>();
                for (int i = 0; i < file_contents.Length; i++)
                {
                    string word = "";

                    //Append the binary word value using the char located at the current file position
                    string_builder.Append(char_to_binary_word_dictionary[file_contents[i]]);

                    //Once we have at least 8 chars, we can construct a byte
                    while (string_builder.Length >= 8)
                    {
                        word = string_builder.ToString().Substring(0, 8);

                        //Remove the word to be constructed from the buffer
                        string_builder.Remove(0, word.Length);
                    }

                    //Convert the word and add it onto the list
                    if (String.IsNullOrEmpty(word) == false)
                        byte_list.Add(Convert.ToByte(word, 2));
                }

                //If there is anything in the buffer, make sure it is retrieved
                if (string_builder.Length > 0)
                {
                    string word = string_builder.ToString();

                    //Convert the word and add it onto the list
                    if (String.IsNullOrEmpty(word) == false)
                        byte_list.Add(Convert.ToByte(word, 2));
                }
                
                BinaryReader BaseFile = new BinaryReader(File.OpenRead(file_name));
                double result = (double)byte_list.ToArray().Length / (double)BaseFile.BaseStream.Length;

                BaseFile.Close();
                return result;
            }

            return -1;
        }

        private static List<HuffmanNode> Get_Initial_Node_List()
        {
            List<HuffmanNode> get_initial_node_list = new List<HuffmanNode>();

            for (int i = Char.MinValue; i < Char.MaxValue; i++)
            {
                get_initial_node_list.Add(new HuffmanNode((char)(i)));
            }

            return get_initial_node_list;
        }

        private static void Sort_Nodes(HuffmanNode node, List<HuffmanNode> nodes)
        {
            if (nodes.Contains(node) == false)
            {
                nodes.Add(node);
            }

            if (node.Left != null)
            {
                Sort_Nodes(node.Left, nodes);
            }

            if (node.Right != null)
            {
                Sort_Nodes(node.Right, nodes);
            }
        }

        private static List<HuffmanNode> Update_Node_Parents(List<HuffmanNode> nodes)
        {
            //Assign parent nodes
            while (nodes.Count > 1)
            {
                int operation_count = (nodes.Count / 2);
                for (int operation = 0, i = 0, j = 1; operation < operation_count; operation++, i += 2, j += 2)
                {
                    if (j < nodes.Count)
                    {
                        HuffmanNode parent_huffman_node = new HuffmanNode(nodes[i], nodes[j]);
                        nodes.Add(parent_huffman_node);

                        nodes[i] = null;
                        nodes[j] = null;
                    }
                }

                //Remove null nodes
                nodes = nodes
                    .Where(m => (m != null))
                    .OrderBy(m => (m.Weight))   //Choose the lowest weightings first
                    .ToList();
            }

            return nodes;
        }

    }
}
