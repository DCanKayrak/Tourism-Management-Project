using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizm.Entities.Concrete;

namespace Turizm.Business.Concrete.FluentValidation
{
    public class TourValidator : AbstractValidator<Tour>
    {
        public TourValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Tur ismi boş bırakılamaz.");
            RuleFor(p => p.Price).NotEmpty().WithMessage("Tur fiyatı boş bırakılamaz.");
            RuleFor(p => p.StartDate).NotEmpty().WithMessage("Tur başlangıç tarihi boş bırakılamaz.");
            RuleFor(p => p.TourType).NotEmpty().WithMessage("Tur tipi boş bırakılamaz.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Tur açıklaması boş bırakılamaz.");
            RuleFor(p => p.EndDate).NotEmpty().WithMessage("Tur bitiş tarihi boş bırakılamaz.");
            RuleFor(p => p.isAnyMealsIncluded).NotEmpty().WithMessage("Tur ekstraları boş bırakılamaz.");

            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Tur fiyatı 0'dan küçük olamaz.");
        }
    }
}
