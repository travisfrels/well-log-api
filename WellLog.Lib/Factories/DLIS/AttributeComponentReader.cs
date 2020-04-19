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

        private readonly IValueReaderFactory _valueReaderFactory;

        public AttributeComponentReader(IValueReaderFactory identReader)
        {
            _valueReaderFactory = identReader;
        }

        public string ReadAttributeLabel(Stream s, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            if (s == null || s.IsAtEndOfStream()) { return null; }

            var identReader = (IIDENTReader)_valueReaderFactory.GetReader(RepresentationCode.IDENT);
            if (descriptor.DoesAttributeHaveLabel) { return identReader.ReadIDENT(s); }
            if (template == null) { return DEFAULT_LABEL; }
            return template.Label;
        }

        public uint ReadAttributeCount(Stream s, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            if (s == null || s.IsAtEndOfStream()) { return 0; }

            var uvariReader = (IUVARIReader)_valueReaderFactory.GetReader(RepresentationCode.UVARI);
            if (descriptor.DoesAttributeHaveCount) { return uvariReader.ReadUVARI(s); }
            if (template == null) { return DEFAULT_COUNT; }
            return template.Count;
        }

        public byte ReadAttributeRepresentationCode(Stream s, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            if (s == null || s.IsAtEndOfStream()) { return 0; }

            var ushortReader = (IUSHORTReader)_valueReaderFactory.GetReader(RepresentationCode.USHORT);
            if (descriptor.DoesAttributeHaveRepresentationCode) { return ushortReader.ReadUSHORT(s); }
            if (template == null) { return DEFAULT_REP_CODE; }
            return template.RepresentationCode;
        }

        public string ReadAttributeUnits(Stream s, ComponentDescriptor descriptor, AttributeComponent template = null)
        {
            if (s == null || s.IsAtEndOfStream()) { return null; }

            var unitsReader = (IUNITSReader)_valueReaderFactory.GetReader(RepresentationCode.UNITS);
            if (descriptor.DoesAttributeHaveUnits) { return unitsReader.ReadUNITS(s); }
            if (template == null) { return DEFAULT_UNITS; }
            return template.Units;
        }

        public IEnumerable ReadAttributeValue(Stream s, ComponentDescriptor descriptor, byte repCode, uint count)
        {
            if (s == null || s.IsAtEndOfStream()) { return null; }
            if (!descriptor.DoesAttributeHaveValue) { return null; }

            var valueReader = _valueReaderFactory.GetReader((RepresentationCode)repCode);
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
                Value = value,
                Template = template
            };
        }
    }
}
