using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleLive.WebApi.Features
{
    public static class LittleLiveFeatureFlags
    {
        public const string GlobalAccessFlag = "GlobalAccessFlag";
        public const string TrialUserFlag = "TrialUserFlag";
        public const string LatePaymentUserFlag = "LatePaymentUserFlag";

        public const string NormalPlanFlag = "NormalPlanFlag";
        public const string PremiumPlanFlag = "PremiumPlanFlag";
        public const string EnterprisePlanFlag = "EnterprisePlanFlag";

        public const string PercentageUserInSpecificCountryFlag = "PercentageUserInSpecificCountryFlag";
    }

    public static class LittleLiveFilterAlias
    {
        public const string TrialUserFilter = "TargertedUser_Trial";
        public const string LatePaymentUserFilter = "TargertedUser_LatePayment";
        public const string NormalPlanFilter = "LicensePlanning_Normal";
        public const string PremiumPlanFilter = "LicensePlanning_Premium";
        public const string EnterprisePlanFilter = "LicensePlanning_Enterprise";
        public const string PercentageUserInSpecificCountryFilter = "PercentageUserInSpecificCountry";
    }
}

