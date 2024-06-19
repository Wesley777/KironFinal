using Newtonsoft.Json;

namespace Kiron.Application.Models.ThirdPartyModels;

public class RootObject
{
    [JsonProperty(PropertyName = "england-and-wales")]
    public EnglandAndWales EnglandAndWales { get; set; }
    public Scotland Scotland { get; set; }
    [JsonProperty(PropertyName = "northern-ireland")]
    public NorthernIreland NorthernIreland { get; set; }
}
