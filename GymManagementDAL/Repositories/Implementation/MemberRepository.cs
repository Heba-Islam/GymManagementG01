using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Implementation
{
    public class MemberRepository : IMemberRepository
    {
        private readonly GymDbContext dbContext;

        public MemberRepository(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Member> GetAll() => dbContext.Members.ToList();


        public Member? GetById(int id) => dbContext.Members.Find(id)!;

        public int Add(Member member)
        {
            dbContext.Members.Add(member);
            return dbContext.SaveChanges();
        }



        public int Update(Member member)
        {
            dbContext.Members.Update(member);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var Member = dbContext.Members.Find(id);
            if (Member is null) return 0;
            dbContext.Members.Remove(Member);
            return dbContext.SaveChanges();

        }
    }
}
