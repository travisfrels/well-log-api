using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WellLog.Lib.Models
{
    public class ValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }

        public ValidationError() { }

        public ValidationError(object entity, string field, string message)
        {
            if (entity == null)
            {
                Field = field;
            }
            else
            {
                var type = entity.GetType();

                var keyValues = type
                    .GetProperties()
                    .Where(x => x.IsDefined(typeof(KeyAttribute), false))
                    .Select(x => x.GetValue(entity)?.ToString())
                    .ToArray();

                /* output: Type(Key).Field */
                Field = string.Format("{0}({1}).{2}", type.Name, string.Join(",", keyValues), field);
            }

            Message = message;
        }
    }
}