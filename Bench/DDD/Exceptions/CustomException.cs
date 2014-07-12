using System;
using System.Collections;

namespace Bench
{
    public abstract class CustomException : Exception
    {
        protected Exception _exception;

        public CustomException() { }

        public CustomException(Exception e)
        {
            _exception = e;
        }

        public override string Message
        {
            get
            {
                return string.Concat(donnerLeTitreDeLException(), '\n', _exception.Message);
            }
        }

        public override string StackTrace
        {
            get
            {
                return _exception.StackTrace;
            }
        }

        public override IDictionary Data
        {
            get
            {
                return _exception.Data;
            }
        }

        public override string Source
        {
            get
            {
                return _exception.Source;
            }
            set
            {
                _exception.Source = value;
            }
        }

        public override string HelpLink
        {
            get
            {
                return _exception.HelpLink;
            }
            set
            {
                _exception.HelpLink = value;
            }
        }

        protected abstract string donnerLeTitreDeLException();
    }
}
