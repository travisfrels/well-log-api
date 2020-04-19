using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class ExplicitlyFormattedLogicalRecordBusiness : IExplicitlyFormattedLogicalRecordBusiness
    {
        private readonly IComponentBusiness _componentBusiness;

        public ExplicitlyFormattedLogicalRecordBusiness(IComponentBusiness componentBusiness)
        {
            _componentBusiness = componentBusiness;
        }

        public ExplicitlyFormattedLogicalRecord ReadExplicitlyFormattedLogicalRecord(Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }

            var eflr = new ExplicitlyFormattedLogicalRecord();

            ComponentBase nextComponent = _componentBusiness.ReadComponent(dlisStream);
            if (!(nextComponent is SetComponent)) { throw new Exception("an EFLR must start with a set component"); }

            eflr.Set = (SetComponent)nextComponent;

            var template = new List<AttributeComponent>();
            nextComponent = _componentBusiness.ReadComponent(dlisStream);
            while (nextComponent != null && nextComponent is AttributeComponent)
            {
                template.Add((AttributeComponent)nextComponent);
                nextComponent = _componentBusiness.ReadComponent(dlisStream);
            }
            if (template.Count < 1) { throw new Exception("an EFLR template must have at least one attribute"); }

            eflr.Template = template.ToArray();

            if (!(nextComponent is ObjectComponent)) { throw new Exception("an EFLR template must be followed by at least one object"); }

            ObjectComponent obj = (ObjectComponent)nextComponent;
            var rows = new List<ObjectComponent>();
            while (obj != null)
            {
                var values = new List<AttributeComponent>();
                foreach (AttributeComponent attr in eflr.Template.Where(x => !x.Descriptor.IsInvariantAttribute))
                {
                    nextComponent = _componentBusiness.ReadComponent(dlisStream, attr);
                    if (nextComponent is SetComponent || nextComponent is ObjectComponent) { throw new Exception("an EFLR object must contain all non-invariant attributes"); }
                    values.Add((AttributeComponent)nextComponent);
                }
                obj.Attributes = values.ToArray();
                rows.Add(obj);

                nextComponent = _componentBusiness.ReadComponent(dlisStream);
                if (nextComponent == null) { obj = null; }
                if (nextComponent is ObjectComponent) { obj = (ObjectComponent)nextComponent; }
                if (nextComponent is SetComponent)
                {
                    obj = null;
                    SetComponent nextSet = (SetComponent)nextComponent;
                    dlisStream.Seek(nextSet.StartPosition, SeekOrigin.Begin);
                }
            }
            eflr.Objects = rows.ToArray();

            return eflr;
        }
    }
}
