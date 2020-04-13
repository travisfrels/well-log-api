using System.IO;
using WellLog.Lib.Enumerations.DLIS;
using WellLog.Lib.Factories.DLIS;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class ComponentBusiness : IComponentBusiness
    {
        public const string DEFAULT_LABEL = "";
        public const uint DEFAULT_COUNT = 1;
        public const byte DEFAULT_REP_CODE = 19;
        public const string DEFAULT_UNITS = "";

        private readonly IUSHORTReader _ushortReader;
        private readonly IIDENTReader _identReader;
        private readonly IOBNAMEReader _obnameReader;
        private readonly IUVARIReader _uvariReader;
        private readonly IUNITSReader _unitsReader;

        public ComponentBusiness(IUSHORTReader ushortReader, IIDENTReader identReader, IOBNAMEReader obnameReader, IUVARIReader uvariReader, IUNITSReader unitsReader)
        {
            _ushortReader = ushortReader;
            _identReader = identReader;
            _obnameReader = obnameReader;
            _uvariReader = uvariReader;
            _unitsReader = unitsReader;
        }

        public ComponentBase ReadComponent(Stream dlisStream, AttributeComponent template = null)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }

            var startPosition = dlisStream.Position;
            var descriptor = new ComponentDescriptor(_ushortReader.ReadUSHORT(dlisStream));
            if (descriptor.IsSet || descriptor.IsRedundantSet || descriptor.IsReplacementSet)
            {
                var setComponent = new SetComponent { Descriptor = descriptor, StartPosition = startPosition };
                if (descriptor.DoesSetHaveType) { setComponent.Type = _identReader.ReadIDENT(dlisStream); }
                if (descriptor.DoesSetHaveName) { setComponent.Name = _identReader.ReadIDENT(dlisStream); }
                return setComponent;
            }

            if (descriptor.IsObject)
            {
                var objComponent = new ObjectComponent { Descriptor = descriptor, StartPosition = startPosition };
                if (descriptor.DoesObjectHaveName) { objComponent.Name = _obnameReader.ReadOBNAME(dlisStream); }
                return objComponent;
            }

            if (descriptor.IsAttribute || descriptor.IsInvariantAttribute)
            {
                byte representationCode = DEFAULT_REP_CODE;
                if (descriptor.DoesAttributeHaveRepresentationCode)
                {
                    representationCode = _ushortReader.ReadUSHORT(dlisStream);
                }
                else if (template != null && template.Descriptor.DoesAttributeHaveRepresentationCode)
                {
                    representationCode = template.RepresentationCode;
                }

                uint count = 0;
                if (descriptor.DoesAttributeHaveCount)
                {
                    count = _uvariReader.ReadUVARI(dlisStream);
                }
                else if (template != null && template.Descriptor.DoesAttributeHaveCount)
                {
                    count = template.Count;
                }

                IValueReader valueReader = null;
                if (descriptor.DoesAttributeHaveValue)
                {
                    valueReader = ValueReaderFactory.GetReader((RepresentationCode)representationCode);
                }

                return new AttributeComponent
                {
                    Descriptor = descriptor,
                    StartPosition = startPosition,
                    Label = descriptor.DoesAttributeHaveLabel ? _identReader.ReadIDENT(dlisStream) : (template == null ? DEFAULT_LABEL : template.Label),
                    Count = count,
                    RepresentationCode = representationCode,
                    Units = descriptor.DoesAttributeHaveUnits ? _unitsReader.ReadUNITS(dlisStream) : (template == null ? DEFAULT_UNITS : template.Units),
                    Value = valueReader?.ReadValues(dlisStream, count)
                };
            }

            return null;
        }
    }
}
