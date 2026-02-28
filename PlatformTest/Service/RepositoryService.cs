using Microsoft.EntityFrameworkCore;
using Npgsql;
using PlatformTest.Data;
using PlatformTest.Entities;
using PlatformTest.Enums;
using PlatformTest.Exceptions;
using PlatformTest.Model;

namespace PlatformTest.Service
{
    public class RepositoryService : IRepositoryService
    {
        private readonly ILogger<IRepositoryService> logger;
        private readonly AppDbContext db;

        public RepositoryService(
            ILogger<IRepositoryService> logger,
            AppDbContext _db)
        {
            this.logger = logger;
            this.db = _db;
        }


        /// <inheritdoc/>
        public async Task RegisterInstrument(InstrumentEntity instrument)
        {
            var instrumentExist = await db.instruments.AnyAsync(i => i.DeviceId == instrument.DeviceId);

            if (!instrumentExist)
            {
                logger.LogInformation($"Adding Instrument with device id: {instrument.DeviceId} to the database.");
                db.instruments.Add(instrument);     
            }
            else
            {
                logger.LogInformation($"Updating fields for device id: {instrument.DeviceId}.");
                db.instruments.Update(instrument);
            }

            await db.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<InstrumentEntity> GetInstrumentById(string deviceId)
        {
            var instrument =
                await db.instruments.FirstOrDefaultAsync(i => i.DeviceId == deviceId) ??
                throw new InstrumentNotFoundException(deviceId);

            return instrument;
        }

        /// <inheritdoc/>
        public async Task ResetInstrumentsState()
        {
            logger.LogInformation("Reseting Instruments table to default state");
            await db.instruments.ExecuteUpdateAsync(setters =>
                    setters
                    .SetProperty(i => i.InstrumentState, InstrumentState.Disconnected));
                    
        }

        /// <inheritdoc/>
        public async Task<MeasurementEntity> SaveMeasurement(MeasurementEntity measurement)
        {
            try
            {
                logger.LogInformation("Saving measurement to the database...");
                measurement.MeasuredAt = GenerateTimestamp();
                db.measurements.Add(measurement);
                await db.SaveChangesAsync();
                logger.LogInformation($"Saved measurement with id: {measurement.Id}");
                return measurement;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }  
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<MeasurementEntity>> GetMeasurements()
        {
            logger.LogInformation("Getting measurements from the database...");
            try
            {
                IEnumerable<MeasurementEntity> measurements = await db.measurements.Include(m => m.Instument).ToListAsync();
                return measurements;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed retrieving measurements from database.\n{ex.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<InstrumentEntity>> GetInstruments()
        {
            logger.LogInformation("Getting instruments from the database...");
            try
            {
                IEnumerable<InstrumentEntity> instruments = await db.instruments.ToListAsync();
                return instruments;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed retrieving instruments from database.\n{ex.Message}");
                throw;
            }
        }

        private DateTime GenerateTimestamp()
        {
            var dateTime = DateTime.UtcNow;
            dateTime = new DateTime(
                dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerSecond),
                dateTime.Kind);

            return dateTime;
        }

    }
}
