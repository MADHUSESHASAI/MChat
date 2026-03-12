using MChatBackend.Core.Entities;
using MChatBackend.Core.ReposetryContracts;
using MChatBackend.Infrastrecture.DBContext;
using Microsoft.EntityFrameworkCore;

namespace MChatBackend.Infrastrecture.Reposetries
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetChatHistoryAsync(
            string user1,
            string user2,
            int page,
            int pageSize)
        {
            return await _context.Messages
                .Where(m =>
                    (m.SenderId == user1 && m.ReceiverId == user2) ||
                    (m.SenderId == user2 && m.ReceiverId == user1))
                .OrderByDescending(m => m.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}