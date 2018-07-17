using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Manager
{
    public class CatererInfoManager : ICatererInfoManager
    {
        private ApplicationDbContext _context;
        public CatererInfoManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public Operation<CatererInfoModel> CreateCatererInfo(CatererInfoModel model)
        {
            return Operation.Create(() =>
            {
                //model.Validate();
                var isExists = _context.CatererInfos.Where(c => c.CaterId == model.CaterId).FirstOrDefault();
                if (isExists != null) throw new Exception("catererInfo already exist");

                var entity = model.Create(model);
                _context.CatererInfos.Add(entity);
                _context.SaveChanges();

                return model;
            });
        }
        public Operation<CatererInfoModel[]> GetCatererInfos()
        {
            return Operation.Create(() =>
            {
                var entities = _context.CatererInfos.ToList();

                var models = entities.Select(s => new CatererInfoModel(s)
                {
                    //Restaurant = new RestaurantModel(s.Restaurant)

                }

                ).ToArray();
                return models;
            });
        }
        public Operation<CatererInfoModel> UpdateCatererInfo(CatererInfoModel model)
        {
            return Operation.Create(() =>
            {
                //model.Validate();
                //var isExist = _context.Scores.Where(c => c.ScoreId == model.ScoreId).AsNoTracking().FirstOrDefault();
                var isExist = _context.CatererInfos.Find(model.CaterId);
                if (isExist == null) throw new Exception("catererInfos does not exist");
                var entity = model.Edit(isExist, model);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();


                return model;
            });
        }
        public Operation<CatererInfoModel> GetCatererInfoById(int caterId)
        {
            return Operation.Create(() =>
            {
                var entity = _context.CatererInfos.Find(caterId);
                if (entity != null) throw new Exception("catererInfo does not exist");
                return new CatererInfoModel(entity);

            });
        }
        public Operation DeleteCatererInfo(int id)
        {
            return Operation.Create(() =>
            {
                var entity = _context.CatererInfos.Find(id);
                if (entity == null) throw new Exception("catererInfos does not exist");

                _context.CatererInfos.Remove(entity);
                _context.SaveChanges();
            });
        }
    }
}