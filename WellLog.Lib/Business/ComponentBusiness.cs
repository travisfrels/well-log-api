using System;
using System.Collections;
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

        public string ReadAttributeLabel(Stream dlisStream, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }

            if (descriptor.DoesAttributeHaveLabel) { return _identReader.ReadIDENT(dlisStream); }
            if (template == null) { return DEFAULT_LABEL; }
            return template.Label;
        }

        public uint ReadAttributeCount(Stream dlisStream, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0; }

            if (descriptor.DoesAttributeHaveCount) { return _uvariReader.ReadUVARI(dlisStream); }
            if (template == null) { return DEFAULT_COUNT; }
            return template.Count;
        }

        public byte ReadAttributeRepresentationCode(Stream dlisStream, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return 0; }

            if (descriptor.DoesAttributeHaveRepresentationCode) { return _ushortReader.ReadUSHORT(dlisStream); }
            if (template == null) { return DEFAULT_REP_CODE; }
            return template.RepresentationCode;
        }

        public string ReadAttributeUnits(Stream dlisStream, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }

            if (descriptor.DoesAttributeHaveUnits) { return _unitsReader.ReadUNITS(dlisStream); }
            if (template == null) { return DEFAULT_UNITS; }
            return template.Units;
        }

        public IEnumerable ReadAttributeValue(Stream dlisStream, ComponentDescriptor descriptor, byte repCode, uint count)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }
            if (!descriptor.DoesAttributeHaveValue) { return null; }

            var valueReader = ValueReaderFactory.GetReader((RepresentationCode)repCode);
            if (valueReader == null) { throw new Exception($"no value reader found for representation code {repCode}"); }
            return valueReader.ReadValues(dlisStream, count);
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
                var label = ReadAttributeLabel(dlisStream, descriptor, template);
                var count = ReadAttributeCount(dlisStream, descriptor, template);
                var representationCode = ReadAttributeRepresentationCode(dlisStream, descriptor, template);
                var units = ReadAttributeUnits(dlisStream, descriptor, template);
                var value = ReadAttributeValue(dlisStream, descriptor, representationCode, count);

                return new AttributeComponent
                {
                    Descriptor = descriptor,
                    StartPosition = startPosition,
                    Label = label,
                    Count = count,
                    RepresentationCode = representationCode,
                    Units = units,
                    Value = value
                };
            }

            return null;
        }
    }
}
