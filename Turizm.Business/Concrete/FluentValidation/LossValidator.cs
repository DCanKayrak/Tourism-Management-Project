using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizm.Entities.Concrete;

namespace Turizm.Business.Concrete.FluentValidation
{
    public class LossValidator:AbstractValidator<Loss>
    {
        public LossValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Zarar ismi boş bırakılamaz.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Zarar açıklaması boş bırakılamaz.");
            RuleFor(p => p.Tour).NotEmpty().WithMessage("Tur boş bırakılamaz.");
            RuleFor(p => p.AmountOfLoss).NotEmpty().WithMessage("Zarar tutarı boş bırakılamaz.");

            RuleFor(p => p.AmountOfLoss).GreaterThan(0).WithMessage("Zarar tutarı 0'dan büyük olmalıdır.");

        }
    }
}
