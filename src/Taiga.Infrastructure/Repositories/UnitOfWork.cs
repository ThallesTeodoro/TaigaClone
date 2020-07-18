using System;
using Taiga.Core.Entities;
using Taiga.Core.Interfaces;
using Taiga.Infrastructure.Data;

namespace Taiga.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository _userRepository;
        private IEmailConfirmationCodeRepository _emailConfirmationCodeRepository;
        private IAttemptsQuantityRepository _attemptsQuantityRepository;
        private IProjectRepository _projectRepository;
        private readonly TaigaContext _context;

        public UnitOfWork(TaigaContext context) { _context = context; }
        public void Commit() { _context.SaveChanges(); }
        public void Rollback() { _context.Dispose(); }

        public IUserRepository UserRepository
        {
            get { return _userRepository = _userRepository ?? new UserRepository(_context); }
        }

        public IEmailConfirmationCodeRepository EmailConfirmationCodeRepository
        {
            get { return _emailConfirmationCodeRepository = _emailConfirmationCodeRepository ?? new EmailConfirmationCodeRepository(_context); }
        }

        public IAttemptsQuantityRepository AttemptsQuantityRepository
        {
            get { return _attemptsQuantityRepository = _attemptsQuantityRepository ?? new AttemptsQuantityRepository(_context); }
        }

        public IProjectRepository ProjectRepository
        {
            get { return _projectRepository = _projectRepository ?? new ProjectRepository(_context); }
        }
    }
}
