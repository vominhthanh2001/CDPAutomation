using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Browser
{
    public class CookieModel
    {
        // Fields
        private string _name;
        private DateTime? _expiryDate;
        private object _value;
        private bool _isSecure;
        private bool _isHttpOnly;

        // Properties
        public string Name
        {
            get => _name;
            set => _name = value ?? string.Empty; // Ensure name is not null
        }

        public DateTime? ExpiryDate
        {
            get => _expiryDate;
            set => _expiryDate = value;
        }

        public object Value
        {
            get => _value;
            set => _value = value;
        }

        public bool IsSecure
        {
            get => _isSecure;
            set => _isSecure = value;
        }

        public bool IsHttpOnly
        {
            get => _isHttpOnly;
            set => _isHttpOnly = value;
        }
    }
}
