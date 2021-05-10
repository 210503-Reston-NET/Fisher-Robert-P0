using System;

namespace StoreUI
{
    public interface MyValidate
    {
        string ValidateString (string prompt);
        double ValidateDouble (string prompt);
    }
}