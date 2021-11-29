using System.Globalization;

// ReSharper disable CheckNamespace

namespace System;

public static class DecimalExtensions
{
    public static string ToCurrencyString(this decimal value) => value.ToCurrencyString(CultureInfo.CurrentUICulture);
    public static string ToCurrencyString(this decimal value, CultureInfo culture) => value.ToString("C0", culture);
    public static string ToCurrencyStringNoSign(this decimal value) => value.ToString("0.####");
}