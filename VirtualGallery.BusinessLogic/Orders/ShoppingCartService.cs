using System;
using System.Collections.Generic;
using System.Linq;
using VirtualGallery.BusinessLogic.Configuration;
using VirtualGallery.BusinessLogic.EMail.Interfaces;
using VirtualGallery.BusinessLogic.EMail.Messages;
using VirtualGallery.BusinessLogic.Filtering;
using VirtualGallery.BusinessLogic.Filtering.Sorting;
using VirtualGallery.BusinessLogic.Orders.Interfaces;
using VirtualGallery.BusinessLogic.UnitOfWork;

namespace VirtualGallery.BusinessLogic.Orders
{
    public class ShoppingCartService : BaseService, IShoppingCartService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly IOrderRepository _orderRepository;

        private readonly IMailBox _mailBox;

        public ShoppingCartService(
            IUnitOfWorkFactory unitOfWorkFactory,
            IOrderRepository orderRepository,
            IMailBox mailBox)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _orderRepository = orderRepository;
            _mailBox = mailBox;
        }

        public void Add(Order order)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                order.CreateDate = DateTime.UtcNow;
                order.UpdateDate = order.CreateDate;
                _orderRepository.Add(order);
                unitOfWork.Commit();
            }


            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                order.Lots.ToList().ForEach(l => { l.Picture.Reserved = true; });
                unitOfWork.Commit();
            }

            _mailBox.Send(new Message(order.Email, "You have an order", "In a short time we will make a call to you. Thank you!") { From = AppSettings.MailFrom });
            _mailBox.Send(new Message(AppSettings.MailFrom, "New order added", "Congratulations") { From = AppSettings.MailFrom });
        }

        public void Update(Order order)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                order.UpdateDate = DateTime.UtcNow;
                _orderRepository.Update(order);
                unitOfWork.Commit();
            }
        }

        public void Remove(Order order)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _orderRepository.Delete(order);
                unitOfWork.Commit();
            }
        }

        public IList<Order> Get()
        {
            return _orderRepository.GetAll(new GenericFilter<Order>
            {
                Sorting = new Sorting<Order>
                {
                    OrderByFilter = q => q.OrderByDescending(c => c.Deleted).ThenBy(c => c.UpdateDate)
                }
            });
        }

        public Order GetById(int orderId)
        {
            EnsureValidId(orderId, "orderId");
            return _orderRepository.GetById(orderId);
        }
    }
}
