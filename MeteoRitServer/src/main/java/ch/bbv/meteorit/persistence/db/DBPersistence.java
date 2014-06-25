package ch.bbv.meteorit.persistence.db;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.logging.Logger;

import javax.inject.Singleton;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.joda.time.DateTimeZone;

import ch.bbv.meteorit.bean.DataPoint;
import ch.bbv.meteorit.bean.Persistence;
import ch.bbv.meteorit.entities.City;
import ch.bbv.meteorit.entities.Measurement;
import ch.bbv.meteorit.rest.DataPointFacadeREST;

@Singleton
public class DBPersistence implements Persistence {

	private static Logger LOG = Logger.getLogger(DataPointFacadeREST.class
			.getName());

	@PersistenceContext
	private EntityManager em;
	
	private double temperature = 0;
	private double pressure = 0;
	private int humidity = 0;


	private void updateMeasurement(MeasurementEntity measurement) {
		if (measurement.getPressure() == 0) {
			measurement.setPressure(pressure);
		}
		if (measurement.getTemperature() == 0) {
			measurement.setTemperature(temperature);
		}
		if (measurement.getHumidity() == 0) {
			measurement.setHumidity(humidity);
		}
	}

	private void updateLocalstore(MeasurementEntity measurement) {
		if (measurement.getPressure() != 0) {
			pressure = measurement.getPressure();
		}
		if (measurement.getTemperature() != 0) {
			temperature = measurement.getTemperature();
		}
		if (measurement.getHumidity() != 0) {
			humidity = measurement.getHumidity();
		}
	}
	
	@Override
	public void updateMeasurement(DataPoint value) {
		Date ts = new Date(DateTimeZone.getDefault().convertUTCToLocal(
				new Date().getTime()));
		long timestamp = ts.getTime()/1000;
		SimpleDateFormat format = new SimpleDateFormat("yyyy.MM.dd HH:mm:ss.SSSZ");
		LOG.info("datapoint: " + format.format(ts) + ", id: "
				+ value.getId() + ", type: " + value.getType() + ", value: "
				+ value.getValue());
		List<City> resultList = em.createNamedQuery("City.findBySensorId", City.class).setParameter("sensorId", value.getId()).getResultList();
		String cityName = "";
		if (!resultList.isEmpty()) {
			cityName = resultList.get(0).getCityName();
		}
		List<MeasurementEntity> measurements = em
				.createNamedQuery("MeasurementEntity.findBySensorIdAndTimestamp",
						MeasurementEntity.class).setParameter("sensorId", 14)
				.setParameter("timestamp", timestamp).getResultList();
		if (measurements.size() > 0) {
			MeasurementEntity measurement = measurements.get(0);
			measurement.updateMeasurements(value, timestamp);
			measurement.setCityName(cityName);
			updateMeasurement(measurement);
			em.merge(measurement);
			updateLocalstore(measurement);
		} else {
			MeasurementEntity measurement = new MeasurementEntity();
			measurement.updateMeasurements(value, timestamp);
			measurement.setCityName(cityName);
			updateMeasurement(measurement);
			em.persist(measurement);
			updateLocalstore(measurement);
		}
	}

	@Override
	public Measurement getMeasurement(int sensorId, long timestamp) {
		List<MeasurementEntity> measurements = em
				.createNamedQuery("MeasurementEntity.findLatestBySensorIdAndTimestamp",
						MeasurementEntity.class).setParameter("sensorId", sensorId)
				.setParameter("timestamp", new Date(timestamp)).getResultList();
		if (measurements.size() > 0) {
			return measurements.get(0).getMeasurement();
		}
		return new MeasurementEntity().getMeasurement();
	}

	@Override
	public Measurement getMeasurement(int sensorId) {
		List<MeasurementEntity> measurement = em
				.createNamedQuery("MeasurementEntity.findBySensorId",
						MeasurementEntity.class).setParameter("sensorId", sensorId)
				.getResultList();
		if (measurement.size() > 0) {
			return measurement.get(0).getMeasurement();
		}
		return new MeasurementEntity().getMeasurement();
	}
}
