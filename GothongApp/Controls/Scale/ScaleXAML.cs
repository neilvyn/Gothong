using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Internals;
using GothongApp.Services.Predefined;
using Prism.Mvvm;

namespace GothongApp.Controls.Scale
{
    [Preserve(AllMembers = true)]
    public class ScaleXAML : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter.ToString().Contains("="))
            {
                var paramType = parameter.ToString().Remove(parameter.ToString().IndexOf("=", StringComparison.CurrentCulture));
                var param = parameter.ToString().Remove(0, paramType.Length + 1);

                if (param.Contains("|"))
                {
                    string[] paramPlatform = param.Split('|');
                    param = Device.RuntimePlatform == Device.Android ? paramPlatform[0] : paramPlatform[1];
                }

                double convertedValue;
                float convertedFValue;

                if (paramType.Contains("height"))
                {
                    if (double.TryParse(param, NumberStyles.Number, CultureInfo.InvariantCulture, out convertedValue))
                        return convertedValue.ScaleHeight();
                }
                else if (paramType.Contains("width"))
                {
                    if (double.TryParse(param, NumberStyles.Number, CultureInfo.InvariantCulture, out convertedValue))
                        return convertedValue.ScaleWidth();
                }
                if (paramType.Contains("height-f"))
                {
                    if (float.TryParse(param, NumberStyles.Number, CultureInfo.InvariantCulture, out convertedFValue))
                        return convertedFValue.ScaleHeight();
                }
                else if (paramType.Contains("width-f"))
                {
                    if (float.TryParse(param, NumberStyles.Number, CultureInfo.InvariantCulture, out convertedFValue))
                        return convertedFValue.ScaleWidth();
                }
                else if (paramType.Contains("platform-thickness"))
                {
                    return ConvertToThicknessProperty(param);
                }
                else if (paramType.Contains("thickness"))
                {
                    return ConvertToThicknessProperty(param);
                }
                else if (paramType.Contains("fontSize"))
                {
                    if (double.TryParse(param, NumberStyles.Number, CultureInfo.InvariantCulture, out convertedValue))
                        return convertedValue.ScaleFont();
                }
                else if (paramType.Contains("absolute-WH"))
                {
                    return ConvertToAbsoluteProperty(param);
                }
                else if (paramType.Contains("absolute-NONE"))
                {
                    return ConvertToRectangleProperty(param);
                }

                throw new InvalidOperationException($"Invalid parameters({paramType}). Supported parameters are height, width, thickness, fontSize, absolute-WH, absolute-NONE");
            }

            // default
            if (parameter.ToString().Contains(",") == true)
            {
                return ConvertToThicknessProperty(parameter.ToString());
            }

            return (double.Parse(parameter.ToString()) * (App.ScreenHeight / 568.0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        object ConvertToThicknessProperty(string param)
        {
            double l, t, r, b;
            string[] thickness = param.Split(',');

            if (thickness.Length == 4)
            {
                if (double.TryParse(thickness[0], NumberStyles.Number, CultureInfo.InvariantCulture, out l) && double.TryParse(thickness[1], NumberStyles.Number, CultureInfo.InvariantCulture, out t) && double.TryParse(thickness[2], NumberStyles.Number, CultureInfo.InvariantCulture, out r) && double.TryParse(thickness[3], NumberStyles.Number, CultureInfo.InvariantCulture, out b))
                {
                    return new Thickness(l * (App.ScreenWidth / 320.0), t * (App.ScreenHeight / 568.0), r * (App.ScreenWidth / 320.0), b * (App.ScreenHeight / 568.0));
                }
            }
            throw new InvalidOperationException("Cannot convert thickness");
        }

        object ConvertToPlatformThicknessProperty(string param)
        {
            string[] paramPlatform = param.Split('|');
            double l, t, r, b;
            string[] thickness = Device.RuntimePlatform == Device.Android ? paramPlatform[0].Split(',') : paramPlatform[1].Split(',');


            if (thickness.Length == 4)
            {
                if (double.TryParse(thickness[0], NumberStyles.Number, CultureInfo.InvariantCulture, out l) && double.TryParse(thickness[1], NumberStyles.Number, CultureInfo.InvariantCulture, out t) && double.TryParse(thickness[2], NumberStyles.Number, CultureInfo.InvariantCulture, out r) && double.TryParse(thickness[3], NumberStyles.Number, CultureInfo.InvariantCulture, out b))
                {
                    return new Thickness(l * (App.ScreenWidth / 320.0), t * (App.ScreenHeight / 568.0), r * (App.ScreenWidth / 320.0), b * (App.ScreenHeight / 568.0));
                }
            }
            throw new InvalidOperationException("Cannot convert platform-thickness");
        }

        object ConvertToAbsoluteProperty(string param)
        {
            double l, t, r, b;
            string[] thickness = param.Split(',');

            if (thickness.Length == 4)
            {
                if (double.TryParse(thickness[0], NumberStyles.Number, CultureInfo.InvariantCulture, out l) && double.TryParse(thickness[1], NumberStyles.Number, CultureInfo.InvariantCulture, out t) && double.TryParse(thickness[2], NumberStyles.Number, CultureInfo.InvariantCulture, out r) && double.TryParse(thickness[3], NumberStyles.Number, CultureInfo.InvariantCulture, out b))
                {
                    return new Rectangle(l, t, r * (App.ScreenWidth / 320.0), b * (App.ScreenHeight / 568.0));
                }
            }
            throw new InvalidOperationException("Cannot convert rectangle");
        }

        object ConvertToRectangleProperty(string param)
        {
            double l, t, r, b;
            string[] thickness = param.Split(',');

            if (thickness.Length == 4)
            {
                if (double.TryParse(thickness[0], NumberStyles.Number, CultureInfo.InvariantCulture, out l) && double.TryParse(thickness[1], NumberStyles.Number, CultureInfo.InvariantCulture, out t) && double.TryParse(thickness[2], NumberStyles.Number, CultureInfo.InvariantCulture, out r) && double.TryParse(thickness[3], NumberStyles.Number, CultureInfo.InvariantCulture, out b))
                {
                    return new Rectangle(l * (App.ScreenWidth / 320.0), t * (App.ScreenHeight / 568.0), r * (App.ScreenWidth / 320.0), b * (App.ScreenHeight / 568.0));
                }
            }
            throw new InvalidOperationException("Cannot convert thickness");
        }
    }

    public class ScaleHeight : IMarkupExtension
    {
        public string Value { get; set; } = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var property = (serviceProvider.GetService<IProvideValueTarget>().TargetProperty as BindableProperty).ReturnType;
            if (!string.IsNullOrEmpty(Value))
            {
                if (Value.Contains("|"))
                {
                    string[] paramPlatform = Value.Split('|');
                    Value = Device.RuntimePlatform == Device.Android ? paramPlatform[0] : paramPlatform[1];
                }
                switch (property.ToString())
                {
                    case "System.Int16": case "System.Int32": case "System.Int64": int.TryParse(Value, out int intValue); return (int)intValue.ScaleHeight();
                    case "System.Double": double.TryParse(Value, out double doubleValue); return (double)doubleValue.ScaleHeight();
                    case "System.Single": float.TryParse(Value, out float floatValue); return (float)floatValue.ScaleHeight();
                }
            }
            throw new InvalidOperationException($"Cannot convert Height[{Value}]");
        }
    }

    public class ScaleWidth : IMarkupExtension
    {
        public string Value { get; set; } = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var property = (serviceProvider.GetService<IProvideValueTarget>().TargetProperty as BindableProperty).ReturnType;
            if (!string.IsNullOrEmpty(Value))
            {
                if (Value.Contains("|"))
                {
                    string[] paramPlatform = Value.Split('|');
                    Value = Device.RuntimePlatform == Device.Android ? paramPlatform[0] : paramPlatform[1];
                }
                switch (property.ToString())
                {
                    case "System.Int16": case "System.Int32": case "System.Int64": int.TryParse(Value, out int intValue); return (int)intValue.ScaleWidth();
                    case "System.Double": double.TryParse(Value, out double doubleValue); return (double)doubleValue.ScaleWidth();
                    case "System.Single": float.TryParse(Value, out float floatValue); return (float)floatValue.ScaleWidth();
                }
            }
            throw new InvalidOperationException($"Cannot convert Width[{Value}]");
        }
    }

    public class ScaleGridHeight : IMarkupExtension
    {
        public double? Value { get; set; } = null;
        public double? Android { get; set; } = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return new GridLength((double)(Android ?? Value)?.ScaleHeight(Value));
        }
    }

    public class ScaleGridWidth : IMarkupExtension
    {
        public double? Value { get; set; } = null;
        public double? Android { get; set; } = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return new GridLength((double)(Android ?? Value)?.ScaleWidth(Value));
        }
    }

    public class ScaleHeightDouble : IMarkupExtension
    {
        public double? Value { get; set; } = null;
        public double? Android { get; set; } = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return (double)(Android ?? Value)?.ScaleHeight(Value);
        }
    }

    public class ScaleHeightInt : IMarkupExtension
    {
        public int? Value { get; set; } = null;
        public int? Android { get; set; } = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return (int)(Android ?? Value)?.ScaleHeight(Value);
        }
    }

    public class ScaleHeightFloat : IMarkupExtension
    {
        public float? Value { get; set; } = null;
        public float? Android { get; set; } = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return (float)(Android ?? Value)?.ScaleHeight(Value);
        }
    }

    public class ScaleWidthDouble : IMarkupExtension
    {
        public double? Value { get; set; } = null;
        public double? Android { get; set; } = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return (double)(Android ?? Value)?.ScaleWidth(Value);
        }
    }

    public class ScaleWidthInt : IMarkupExtension
    {
        public int? Value { get; set; } = null;
        public int? Android { get; set; } = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return (int)(Android ?? Value)?.ScaleWidth(Value);
        }
    }

    public class ScaleWidthFloat : IMarkupExtension
    {
        public float? Value { get; set; } = null;
        public float? Android { get; set; } = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return (float)(Android ?? Value)?.ScaleWidth(Value);
        }
    }

    public class ScaleThickness : BindableBase, IMarkupExtension
    {
        public string Value { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (!string.IsNullOrEmpty(Value))
            {
                if (Value.Contains("|"))
                {
                    string[] paramPlatform = Value.Split('|');
                    Value = Device.RuntimePlatform == Device.Android ? paramPlatform[0] : paramPlatform[1];
                }

                ThicknessTypeConverter thicknessTypeConverter = new ThicknessTypeConverter();
                return ((Thickness)thicknessTypeConverter.ConvertFromInvariantString(Value)).ScaleThickness();
            }

            throw new InvalidOperationException("Cannot convert thickness");
        }
    }

    public class ScaleFontSize : IMarkupExtension
    {
        public double? Value { get; set; } = null;
        public double? Android { get; set; } = null;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return (Android ?? Value)?.ScaleFont(Value);
        }
    }

    public class ScaleCornerRadius : IMarkupExtension
    {
        public string Value { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (!string.IsNullOrEmpty(Value))
            {
                if (Value.Contains("|"))
                {
                    string[] paramPlatform = Value.Split('|');
                    Value = Device.RuntimePlatform == Device.Android ? paramPlatform[0] : paramPlatform[1];
                }

                CornerRadiusTypeConverter cornerRadiusTypeConverter = new CornerRadiusTypeConverter();
                return ((CornerRadius)cornerRadiusTypeConverter.ConvertFromInvariantString(Value)).ScaleCornerRadius();
            }
            throw new InvalidOperationException("Cannot convert CornerRadius");
        }
    }
}
