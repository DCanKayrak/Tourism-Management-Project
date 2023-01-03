using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizm.Entities.Concrete;

namespace Turizm.Business.Concrete.FluentValidation
{
    public class CustomerValidator:AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("İsim boş bırakılamaz.");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Soyadı boş bırakılamaz.");
            RuleFor(p => p.NationalityId).NotEmpty().WithMessage("TC kimlik numarası boş bırakılamaz.");
            RuleFor(p => p.Mail).NotEmpty().WithMessage("Mail boş bırakılamaz.");
            RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.");
            RuleFor(p => p.RegisteredTour).NotEmpty().WithMessage("Kaydolmak istediği tur boş bırakılamaz.");
            RuleFor(p => p.Birthday).NotEmpty().WithMessage("Doğum günü tarihi boş bırakılamaz.");

            RuleFor(p => p.PhoneNumber).Must(CheckPhoneNumber).WithMessage("Telefon Numarası 0 ile başlamalı. Örnek : 05554957384");
            RuleFor(p => p.NationalityId).Must(CheckNationalityId).WithMessage("TC kimlik numarası 11 rakamdan oluşmalı.");
            RuleFor(p => p.Mail).Must(CheckMail).WithMessage("Mail adresiniz @gmail.com veya @outlook.com adresleri ile sonlanmalıdır.");
        }
        private bool CheckMail(string str)
        {
            return str.EndsWith("@gmail.com") || str.EndsWith("@outlook.com");
        }
        private bool CheckNationalityId(string str)
        {
            try
            {
                Convert.ToDecimal(str);
            }catch(Exception ex)
            {
                return false;
            }
            return str.Length == 11;
        }
        private bool CheckPhoneNumber(string str)
        {
            return str.StartsWith("0") && str.Length == 11;
        }
    }
}
