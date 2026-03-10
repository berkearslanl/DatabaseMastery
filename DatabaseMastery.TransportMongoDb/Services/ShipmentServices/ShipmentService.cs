using AutoMapper;
using DatabaseMastery.TransportMongoDb.Dtos.ShipmentDtos;
using DatabaseMastery.TransportMongoDb.Entities;
using DatabaseMastery.TransportMongoDb.Settings;
using MongoDB.Driver;

namespace DatabaseMastery.TransportMongoDb.Services.ShipmentServices
{
    public class ShipmentService : IShipmentService
    {
        private readonly IMongoCollection<Shipment> _ShipmentCollection;
        private readonly IMapper _mapper;

        public ShipmentService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _ShipmentCollection = database.GetCollection<Shipment>(_databaseSettings.ShipmentCollectionName);
            _mapper = mapper;
        }

        public async Task CreateShipmentAsync(CreateShipmentDto createShipmentDto)
        {
            var value = _mapper.Map<Shipment>(createShipmentDto);
            await _ShipmentCollection.InsertOneAsync(value);
        }

        public async Task DeleteShipment(string id)
        {
            await _ShipmentCollection.DeleteOneAsync(x => x.ShipmentId == id);
        }

        public async Task<List<ResultShipmentDto>> GetAllShipmentAsync()
        {
            var value = await _ShipmentCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultShipmentDto>>(value);
        }
        //filtreli veri sayısı
        public async Task<long> GetDeliveredShipmentCountAsync()
        {
            var filter = Builders<Shipment>.Filter.Eq(x => x.CurrentStatus, "Teslim Edildi");
            return await _ShipmentCollection.CountDocumentsAsync(filter);
        }

        public async Task<int> GetDistinctDestinationCityCountAsync()
        {
            var cities = await _ShipmentCollection.DistinctAsync<string>("DestinationCity", FilterDefinition<Shipment>.Empty);
            return await cities.ToListAsync().ContinueWith(t => t.Result.Count);
        }

        public async Task<GetShipmentByIdDto> GetShipmentByIdAsync(string id)
        {
            var value = await _ShipmentCollection.Find(x => x.ShipmentId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetShipmentByIdDto>(value);
        }

        public async Task<GetShipmentByIdDto> GetShipmentByTrackingNumberAsync(string trackingNumber)
        {
            var value = await _ShipmentCollection.Find(x => x.TrackingNumber == trackingNumber).FirstOrDefaultAsync();

            return _mapper.Map<GetShipmentByIdDto>(value);
        }

        //veri sayısı
        public async Task<long> GetTotalShipmentCountAsync()
        {
            return await _ShipmentCollection.CountDocumentsAsync(FilterDefinition<Shipment>.Empty);//filtre olmadan hepsini al
        }

        public async Task UpdateShipmentAsync(UpdateShipmentDto updateShipmentDto)
        {
            var value = _mapper.Map<Shipment>(updateShipmentDto);
            await _ShipmentCollection.FindOneAndReplaceAsync(x => x.ShipmentId == updateShipmentDto.ShipmentId, value);
        }
    }
}
