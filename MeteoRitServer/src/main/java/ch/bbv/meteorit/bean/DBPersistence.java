package ch.bbv.meteorit.bean;

import java.util.Date;
import java.util.List;
import java.util.logging.Logger;

import javax.inject.Singleton;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.joda.time.DateTimeZone;

import ch.bbv.meteorit.entities.City;
import ch.bbv.meteorit.entities.Measurement;
import ch.bbv.meteorit.rest.DataPointFacadeREST;

@Singleton
public class DBPersistence implements Persistence {

	private static Logger LOG = Logger.getLogger(DataPointFacadeREST.class
			.getName());

	@PersistenceContext
	private EntityManager em;

	@Override
	public void updateMeasurement(DataPoint value) {
		Date timestamp = new Date(DateTimeZone.getDefault().convertUTCToLocal(
				new Date().getTime()));
		LOG.info("datapoint: " + timestamp.toString() + ", id: "
				+ value.getId() + ", type: " + value.getType() + ", vallue: "
				+ value.getValue());
		List<City> resultList = em.createNamedQuery("City.findBySensorId", City.class).setParameter("sensorId", value.getId()).getResultList();
		String cityName = "";
		if (!resultList.isEmpty()) {
			cityName = resultList.get(0).getCityName();
		}
		List<Measurement> measurements = em
				.createNamedQuery("Measurement.findBySensorIdAndTimestamp",
						Measurement.class).setParameter("sensorId", 14)
				.setParameter("timestamp", timestamp).getResultList();
		if (measurements.size() > 0) {
			Measurement measurement = measurements.get(0);
			measurement.updateMeasurements(value, timestamp);
			measurement.setCityName(cityName);
			em.merge(measurement);
		} else {
			Measurement measurement = new Measurement();
			measurement.updateMeasurements(value, timestamp);
			measurement.setCityName(cityName);
			em.persist(measurement);
		}
	}

	@Override
	public Measurement getMeasurement(int sensorId, long timestamp) {
		List<Measurement> measurement = em
				.createNamedQuery("Measurement.findLatestBySensorIdAndTimestamp",
						Measurement.class).setParameter("sensorId", sensorId)
				.setParameter("timestamp", new Date(timestamp)).getResultList();
		if (measurement.size() > 0) {
			return measurement.get(0);
		}
		return new Measurement();
	}

	@Override
	public Measurement getMeasurement(int sensorId) {
		List<Measurement> measurement = em
				.createNamedQuery("Measurement.findBySensorId",
						Measurement.class).setParameter("sensorId", sensorId)
				.getResultList();
		if (measurement.size() > 0) {
			return measurement.get(0);
		}
		return new Measurement();
	}
}
