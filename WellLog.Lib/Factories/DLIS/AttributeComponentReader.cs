using System;
using System.Collections;
using System.IO;
using WellLog.Lib.Enumerations.DLIS;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Factories.DLIS
{
    public class AttributeComponentReader : IComponentReader, IAttributeComponentReader
    {
        public const string DEFAULT_LABEL = "";
        public const uint DEFAULT_COUNT = 1;
        public const byte DEFAULT_REP_CODE = 19;
        public const string DEFAULT_UNITS = "";

        private readonly IIDENTReader _identReader;
        private readonly IUVARIReader _uvariReader;
        private readonly IUSHORTReader _ushortReader;
        private readonly IUNITSReader _unitsReader;

        public AttributeComponentReader(IIDENTReader identReader, IUVARIReader uvariReader, IUSHORTReader ushortReader, IUNITSReader unitsReader)
        {
            _identReader = identReader;
            _uvariReader = uvariReader;
            _ushortReader = ushortReader;
            _unitsReader = unitsReader;
        }

        public string ReadAttributeLabel(Stream s, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            if (s == null || s.IsAtEndOfStream()) { return null; }

            if (descriptor.DoesAttributeHaveLabel) { return _identReader.ReadIDENT(s); }
            if (template == null) { return DEFAULT_LABEL; }
            return template.Label;
        }

        public uint ReadAttributeCount(Stream s, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            if (s == null || s.IsAtEndOfStream()) { return 0; }

            if (descriptor.DoesAttributeHaveCount) { return _uvariReader.ReadUVARI(s); }
            if (template == null) { return DEFAULT_COUNT; }
            return template.Count;
        }

        public byte ReadAttributeRepresentationCode(Stream s, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            if (s == null || s.IsAtEndOfStream()) { return 0; }

            if (descriptor.DoesAttributeHaveRepresentationCode) { return _ushortReader.ReadUSHORT(s); }
            if (template == null) { return DEFAULT_REP_CODE; }
            return template.RepresentationCode;
        }

        public string ReadAttributeUnits(Stream s, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            if (s == null || s.IsAtEndOfStream()) { return null; }

            if (descriptor.DoesAttributeHaveUnits) { return _unitsReader.ReadUNITS(s); }
            if (template == null) { return DEFAULT_UNITS; }
            return template.Units;
        }

        public IEnumerable ReadAttributeValue(Stream s, ComponentDescriptor descriptor, byte repCode, uint count)
        {
            if (s == null || s.IsAtEndOfStream()) { return null; }
            if (!descriptor.DoesAttributeHaveValue) { return null; }

            var valueReader = ValueReaderFactory.GetReader((RepresentationCode)repCode);
            if (valueReader == null) { throw new Exception($"no value reader found for representation code {repCode}"); }
            return valueReader.ReadValues(s, count);
        }

        public ComponentBase ReadComponent(Stream s, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            var label = ReadAttributeLabel(s, descriptor, template);
            var count = ReadAttributeCount(s, descriptor, template);
            var representationCode = ReadAttributeRepresentationCode(s, descriptor, template);
            var units = ReadAttributeUnits(s, descriptor, template);
            var value = ReadAttributeValue(s, descriptor, representationCode, count);

            return new AttributeComponent
            {
                Descriptor = descriptor,
                Label = label,
                Count = count,
                RepresentationCode = representationCode,
                Units = units,
                Value = value
            };
        }
    }
}
