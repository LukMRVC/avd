using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TupleAtATime
{
    class Filter : BasicOperator
    {
        private readonly BasicOperator _input;
        private readonly BasicOperator _filter;
        // private List<XmlNode> _elements;
        // private int _elementPosition;

        public Filter(BasicOperator input, BasicOperator filter)
        {
            this._input = input;
            this._filter = filter;
        }

        public override void SetContext(XmlNode context)
        {
            _input.SetContext(context);
        }

        public override bool MoveNext()
        {
            if (!IsOpen)
            {
                IsOpen = true;
            }

            while (IsOpen)
            {

                if (!_input.MoveNext())
                {
                    // break out of while(IsOpen)
                    break;
                }

                _filter.SetContext(_input.Current);
                if (_filter.MoveNext())
                {
                    Current = _input.Current;
                    _filter.Reset();
                    return true;
                }
            }

            IsOpen = false;
            return false;
        }

        public override void Reset()
        {
            _input.Reset();
            _filter.Reset();
            IsOpen = false;
        }


        public override string ToString()
        {
            return _input.ToString() + "[" + _filter.ToString() + "]";
        }
    }
}
