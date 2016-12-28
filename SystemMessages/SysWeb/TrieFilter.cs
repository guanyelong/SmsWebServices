using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysWeb
{
    public class TrieNode
    {
        public bool m_end;
        public Dictionary<Char, TrieNode> m_values;
        public TrieNode()
        {
            m_values = new Dictionary<Char, TrieNode>();
        }
    }

    public class TrieFilter : TrieNode
    {
        /// <summary>
        /// 添加关键字
        /// </summary>
        /// <param name="key"></param>
        public void AddKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            TrieNode node = this;
            for (int i = 0; i < key.Length; i++)
            {
                char c = key[i];
                TrieNode subnode;
                if (!node.m_values.TryGetValue(c, out subnode))
                {
                    subnode = new TrieNode();
                    node.m_values.Add(c, subnode);
                }
                node = subnode;
            }
            node.m_end = true;
        }

        /// <summary>
        /// 检查是否包含非法字符
        /// </summary>
        /// <param name="text">输入文本</param>
        /// <returns>找到的第1个非法字符.没有则返回string.Empty</returns>
        public bool HasBadWord(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                TrieNode node;
                if (m_values.TryGetValue(text[i], out node))
                {
                    for (int j = i + 1; j < text.Length; j++)
                    {
                        if (node.m_values.TryGetValue(text[j], out node))
                        {
                            if (node.m_end)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 检查是否包含非法字符
        /// </summary>
        /// <param name="text">输入文本</param>
        /// <returns>找到的第1个非法字符.没有则返回string.Empty</returns>
        public string FindOne(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                TrieNode node;
                if (m_values.TryGetValue(c, out node))
                {
                    for (int j = i + 1; j < text.Length; j++)
                    {
                        if (node.m_values.TryGetValue(text[j], out node))
                        {
                            if (node.m_end)
                            {
                                return text.Substring(i, j + 1 - i);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return string.Empty;
        }

        //查找所有非法字符
        public IEnumerable<string> FindAll(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                TrieNode node;
                if (m_values.TryGetValue(text[i], out node))
                {
                    for (int j = i + 1; j < text.Length; j++)
                    {
                        if (node.m_values.TryGetValue(text[j], out node))
                        {
                            if (node.m_end)
                            {
                                yield return text.Substring(i, (j + 1 - i));
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 替换非法字符
        /// </summary>
        /// <param name="text"></param>
        /// <param name="c">用于代替非法字符</param>
        /// <returns>替换后的字符串</returns>
        public string Replace(string text, char c)
        //public string Replace(string text, char c = '*')
        {
            char[] chars = null;
            for (int i = 0; i < text.Length; i++)
            {
                TrieNode subnode;
                if (m_values.TryGetValue(text[i], out subnode))
                {
                    for (int j = i + 1; j < text.Length; j++)
                    {
                        if (subnode.m_values.TryGetValue(text[j], out subnode))
                        {
                            if (subnode.m_end)
                            {
                                if (chars == null) chars = text.ToArray();
                                for (int t = i; t <= j; t++)
                                {
                                    chars[t] = c;
                                }
                                i = j;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return chars == null ? text : new string(chars);
        }
    }
}