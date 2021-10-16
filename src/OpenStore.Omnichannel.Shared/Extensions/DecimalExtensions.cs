using System.Globalization;

// ReSharper disable CheckNamespace

namespace System;

public static class DecimalExtensions
{
    public static string ToCurrencyString(this decimal value) => value.ToString("C0", CultureInfo.CurrentUICulture);
}