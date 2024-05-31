using OnlineBankingApp.BusinessLayer.Abstract;
using OnlineBankingApp.BusinessLayer.MQService;
using OnlineBankingApp.DataLayer.Abstract;
using OnlineBankingApp.Entities.Concrete;

namespace OnlineBankingApp.BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly MessageQueueService _messageQueueService;

        public UserManager(IUserDal userDal, MessageQueueService messageQueueService)
        {
            _userDal = userDal;
            _messageQueueService = messageQueueService;
        }

        public async Task<Result> TDelete(int id)
        {
            var value = _userDal.GetById(id);
            if (value is not null)
            {
                await _userDal.Delete(id);
                _messageQueueService.PublishMessage($"User Deleted: {id}");
                return new Result { Success = true };
            }
            else
            {
                return new Result
                {
                    Success = false,
                    ErrorMessage = "object not found"
                };
            }

        }

        public async Task<List<User>> TGetAll()
        {
            return await _userDal.GetAll();
        }

        public Task<User> TGetById(int id)
        {
            return _userDal.GetById(id);
        }

        public async Task TInsert(User entity)
        {
            await _userDal.Insert(entity);
            _messageQueueService.PublishMessage($"User Created: {entity}");
        }

        public async Task TUpdate(User entity)
        {
            await _userDal.Update(entity);
            _messageQueueService.PublishMessage($"User Updated: {entity}");
        }
    }
}
