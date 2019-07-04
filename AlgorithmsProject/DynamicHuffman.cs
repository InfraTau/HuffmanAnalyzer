using System;
using System.IO;

namespace AlgorithmsProject
{
    public class DynamicHuffman
    {
        private static BinaryReader _source_file;

        public static double Encode(string file_name)
        {
            bool opened = false;
            BinaryWriter output_file = null;

            try
            {
                _source_file = new BinaryReader(File.OpenRead(file_name));
                output_file = new BinaryWriter(File.Create($@"{System.Windows.Forms.Application.StartupPath}\output.dynhuf"));
                opened = true;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }

            if (opened)
            {
                DynamicHuffmanTree encoding_model = new DynamicHuffmanTree();
                encoding_model.CreateModel();

                bool end_of_file = false;
                Byte symbol = 0;
                string code = "";
                string buffer = "";

                char[] CharTypeBuffer;
                FileInfo file_info = new FileInfo(file_name);
                CharTypeBuffer = (file_info.Extension.Substring(1, file_info.Extension.Length - 1) + ".").ToCharArray();
                output_file.Write(CharTypeBuffer);

                while (!end_of_file)
                {
                    try
                    {
                        symbol = _source_file.ReadByte();
                    }
                    catch
                    {
                        end_of_file = true;
                    }

                    if (!end_of_file)
                        code = encoding_model.Encode(symbol);
                    else
                        code = encoding_model.Encode(null);

                    code = buffer + code;
                    buffer = code.Substring(code.Length - (code.Length % 8), (code.Length) - (code.Length - (code.Length % 8)));
                    code = code.Remove(code.Length - (code.Length % 8), (code.Length) - (code.Length - (code.Length % 8)));

                    while (code != "")
                    {
                        output_file.Write(DynamicHuffmanTree.ToByte(code.Substring(0, 8)));
                        code = code.Remove(0, 8);
                    }

                    if (end_of_file)
                    {
                        while (buffer.Length != 8) buffer += "0";
                        output_file.Write(DynamicHuffmanTree.ToByte(buffer));
                    }

                }

                _source_file.Close();
                output_file.Close();

                BinaryReader ResultFile = new BinaryReader(File.OpenRead($@"{System.Windows.Forms.Application.StartupPath}\output.dynhuf"));
                BinaryReader BaseFile = new BinaryReader(File.OpenRead(file_name));  

                double result = (double)ResultFile.BaseStream.Length / (double)BaseFile.BaseStream.Length;

                ResultFile.Close();
                BaseFile.Close();
                return result;
            }

            return -1;
        }
    }
}