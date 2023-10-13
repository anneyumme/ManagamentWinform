namespace DevExpress.UITemplates.Collection.Models {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Entity<TKey> {
        [Key]
        [Display(Order = -1)]
        public TKey ID {
            get;
            set;
        }
        public override bool Equals(object obj) {
            Entity<TKey> entity = obj as Entity<TKey>;
            if(ReferenceEquals(entity, null))
                return false;
            return EqualityComparer<TKey>.Default.Equals(ID, entity.ID);
        }
        public virtual void Assign(Entity<TKey> source) {
            if(source != null) {
                ID = source.ID;
            }
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
