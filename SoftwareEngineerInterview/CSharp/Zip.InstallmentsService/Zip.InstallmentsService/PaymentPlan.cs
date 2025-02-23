﻿using Microsoft.AspNetCore.Mvc;
using System;

namespace Zip.InstallmentsService.Model
{
    /// <summary>
    /// Data structure which defines all the properties for a purchase installment plan.
    /// </summary>
    public class PaymentPlan
    {
        public Guid Id { get; set; }

		public decimal PurchaseAmount { get; set; }

        public Installment[] Installments { get; set; }

        public static explicit operator ObjectResult(PaymentPlan v)
        {
            throw new NotImplementedException();
        }
    }
}
