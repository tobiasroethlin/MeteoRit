package ch.bbv.meteorit.persistence.couchbase;

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.Arrays;
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

import com.couchbase.client.CouchbaseClient;
import com.google.gson.Gson;

@Singleton
public class CouchbasePersistence implements Persistence {

	private static Logger LOG = Logger.getLogger(DataPointFacadeREST.class
			.getName());

	private CouchbaseClient client;

	private double temperature = 0;
	private double pressure = 0;
	private int humidity = 0;

	@PersistenceContext
	private EntityManager em;

	public CouchbasePersistence() throws URISyntaxException, IOException {
		List<URI> hosts = Arrays.asList(new URI(
				"http://192.168.1.186:8091/pools"));
		String bucket = "meteoR";
		String password = "";
		client = new CouchbaseClient(hosts, bucket, password);
	}

	private void updateMeasurement(Measurement measurement) {
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

	private void updateLocalstore(Measurement measurement) {
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
		long timestamp = ts.getTime() / 1000;
		LOG.info("datapoint: " + timestamp + ", id: " + value.getId()
				+ ", type: " + value.getType() + ", vallue: "
				+ value.getValue());
		List<City> resultList = em
				.createNamedQuery("City.findBySensorId", City.class)
				.setParameter("sensorId", value.getId()).getResultList();
		String cityName = "";
		if (!resultList.isEmpty()) {
			cityName = resultList.get(0).getCityName();
		}

		String couchbaseKey = cityName + ":" + timestamp;
		Measurement measurement = new Measurement();
		measurement.updateMeasurements(value, timestamp);
		measurement.setCityName(cityName);
		client.set(couchbaseKey, new Gson().toJson(measurement));
	}

	@Override
	public Measurement getMeasurement(int sensorId, long timestamp) {
		return null;
	}

	@Override
	public Measurement getMeasurement(int sensorId) {
		return null;
	}

}
