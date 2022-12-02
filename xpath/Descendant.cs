using System.Collections.Generic;
using System.Xml;

namespace TupleAtATime
{
    class Descendant : BasicOperator
    {
        private readonly BasicOperator _input;
        private readonly string _tagName;
        private Stack<XmlNode> stack;

        public Descendant(BasicOperator input, string tagName)
        {
            this._input = input;
            this._tagName = tagName;
            stack = new Stack<XmlNode>();
        }

        public override void SetContext(XmlNode context)
        {
            _input.SetContext(context);
        }

        public override bool MoveNext()
        {
            if (!IsOpen)
            {
                // we open the input operator
                IsOpen = true;
                if (!_input.MoveNext())
                {

                    IsOpen = false;
                    return false;
                }
                else
                {
                    for (int i = _input.Current.ChildNodes.Count - 1; i >= 0; i--)
                    {
                        if (_input.Current.ChildNodes[i].Name != "#text")
                        {
                            stack.Push(_input.Current.ChildNodes[i]);
                        }
                    }
                }

            }

            while (IsOpen)
            {
                while (stack.Count > 0)
                {
                    var subtree = stack.Pop();
                    for (int i = subtree.ChildNodes.Count - 1; i >= 0; i--)
                    {
                        stack.Push(subtree.ChildNodes[i]);
                    }
                    if (subtree.Name == _tagName)
                    {
                        Current = subtree;
                        return true;
                    }
                }

                if (!_input.MoveNext())
                {
                    break;
                }

                for (int i = _input.Current.ChildNodes.Count - 1; i >= 0; i--)
                {
                    stack.Push(_input.Current.ChildNodes[i]);
                }

            }

            IsOpen = false;
            return false;
        }

        public override void Reset()
        {
            _input.Reset();
            stack.Clear();
            IsOpen = false;
        }

        public override string ToString()
        {
            return _input.ToString() + "//" + _tagName;
        }
    }
}
