﻿namespace Base.Utility.Guards.GuardClauses;
public static class NotNullGuardClause
{
    /// <summary>
    /// بررسی اینکه آیا مقدار ورودی `null` نیست و در صورت `null` بودن، استثنا پرتاب می‌شود.
    /// </summary>
    /// <param name="guard">نمونه‌ای از کلاس Guard برای اجرای گاردها</param>
    /// <param name="value">مقدار ورودی که باید بررسی شود</param>
    /// <param name="message">پیام خطا که در صورت `null` بودن مقدار ورودی، برای پرتاب استثنا استفاده می‌شود</param>
    /// <exception cref="ArgumentNullException">در صورت خالی بودن پیام</exception>
    /// <exception cref="InvalidOperationException">در صورت `null` بودن مقدار ورودی</exception>
    public static void Null<T>(this Guard guard, T value, string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentNullException("Message");

        if (value == null)
            throw new InvalidOperationException(message);
    }
}
