using System.Xml;

namespace TupleAtATime
{
    class And : BasicOperator
    {
        private readonly BasicOperator first;
        private readonly BasicOperator second;

        public And(BasicOperator _first, BasicOperator _second)
        {
            this.first = _first;
            this.second = _second;
        }

        public override void SetContext(XmlNode context)
        {
            first.SetContext(context);
            second.SetContext(context);
        }

        public override bool MoveNext()
        {
            // return true when both first and second return true
            if (!IsOpen)
            {
                IsOpen = true;
            }

            while (IsOpen)
            {
                // TODO: While second move next!
                while (first.MoveNext())
                {
                    if (second.MoveNext())
                    {
                        Current = first.Current;
                        return true;
                    }
                    second.Reset();
                }
                break;
            }
            IsOpen = false;
            return false;
        }

        public override void Reset()
        {
            first.Reset();
            second.Reset();
            IsOpen = false;
        }

        public override string ToString()
        {
            return first.ToString() + " and " + second.ToString();
        }
    }
}