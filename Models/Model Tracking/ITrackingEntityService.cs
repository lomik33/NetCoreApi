using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreApi.Models{
    public interface ITrackingEntityService<TEntity, TEditRequest>{
        IEnumerable<ChangeLog<TEntity>> GetChanges (TEntity oldEntity, TEditRequest newEntity){
            List<ChangeLog<TEntity>> changes = new List<ChangeLog<TEntity>>();
            var oldProperties = oldEntity.GetType().GetProperties();
            var newProperties = newEntity.GetType().GetProperties();
            foreach (var newProperty in newProperties){
                if (newProperty.GetValue(newEntity) == null) continue;
                var oldProperty = oldProperties.FirstOrDefault(x => x.Name == newProperty.Name);
                if (oldProperty == null) continue;
                if (newProperty.GetValue(newEntity) is Guid g && g.Equals(Guid.Empty)) continue;
                //if (oldProperty.GetValue(oldEntity)?.ToString() == newProperty.GetValue(newEntity)?.ToString()) continue;
                if (oldProperty.GetValue(oldEntity).Equals(newProperty.GetValue(newEntity))) continue;
                                ChangeLog<TEntity> change = new ChangeLog<TEntity>(){
                    PropertyName = newProperty.Name,
                    NewValue = newProperty.GetValue(newEntity),
                    OldValue = oldProperty.GetValue(oldEntity),
                };
                changes.Add(change);
            }
            return changes;
        }
        void GetModifiedResult(ref TEntity oldEntity, IEnumerable<ChangeLog<TEntity>> changes){
            foreach (var change in changes ?? Enumerable.Empty<ChangeLog<TEntity>>()){
                var prop = oldEntity.GetType().GetProperty(change.PropertyName);
                if (prop == null) continue;
                prop.SetValue(oldEntity,change.NewValue);
            }
        }
    }
}