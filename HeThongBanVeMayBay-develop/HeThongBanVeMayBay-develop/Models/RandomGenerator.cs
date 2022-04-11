using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HeThongBanVeMayBay.Models
{
    public class RandomGenerator
    {
        public const int DEFAULT_CAPACITY = 6;
        private string[] _store;
        private Random _rdmizer;
        private int _capacity;
        private static int _current = 0;

        /*
        * PROPERTIES
        */
        public int Capacity
        {
            get { return _capacity; }
            set { this._capacity = value; }
        }

        public int Current
        {
            get { return _current; }
        }

        /*
        * CONSTRUCTOR
        */
        public RandomGenerator()
        {
            _store = new string[100];
            for (int cnt = 0; cnt < 100; cnt++)
                _store[cnt] = "";
            _rdmizer = new Random();
            _capacity = DEFAULT_CAPACITY;
        }
        // Just only use to generate a random string
        public string Generate()
        {
            StringBuilder builder = new StringBuilder();
            while (builder.Length < _capacity)
            {
                builder.Append(_GenerateChar());
            }
            return builder.ToString();
        }

        // Add a string to store
        public void Add(string _in_str)
        {
            _store[_current++] = _in_str;
        }
        // Generate the next string to the store
        public string Next()
        {
            string _str = Generate();
            if (IsStored(_str) == true)
                Next();
            Add(_str);
            return _str;
        }
        // check whether a string stored already
        public bool IsStored(string _in_str)
        {
            for (int _cnt = 0; _cnt < _store.Length; _cnt++)
            {
                if (_store[_cnt].CompareTo(_in_str) == 0)
                    return true;
            }
            return false;
        }
        // get all stored strings
        public string[] GetStore()
        {
            return _store;
        }
        // generate a random character
        private char _GenerateChar()
        {
            int _num;
            do
            {
                _num = _rdmizer.Next(48, 122);
            } while ((_num < 48) || (_num > 57 && _num < 65) || (_num > 90 && _num < 97));

            char chr = Convert.ToChar(_num);
            return chr;
        }
    }
}