using Foundation;
using UIKit;
using ObjCRuntime;
using System;

namespace iZettle
{
    [Native]
    public enum IZSDKErrorCode : long
    {
        UserNotLoggedIn = -1,
        PaymentNotFound = -100,
        ReferenceTooLong = -101,
        ReferenceIsNil = -102,
        OperationAlreadyInProgress = -300,
        InvalidAmount = -400,
        AmountTooLow = -401,
        AmountTooHigh = -402
    }



}