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
import ch.bbv.meteorit.persistence.db.Measurement;
import ch.bbv.meteorit.rest.DataPointFacadeREST;

import com.couchbase.client.CouchbaseClient;

@Singleton
public class CouchbasePersistence implements Persistence {

	private static Logger LOG = Logger.getLogger(DataPointFacadeREST.class
			.getName());

	CouchbaseClient client;

	@PersistenceContext
	private EntityManager em;

	public CouchbasePersistence() throws URISyntaxException, IOException {
		List<URI> hosts = Arrays.asList(new URI(
				"http://192.168.1.141:8091/pools"));
		String bucket = "default";
		String password = "";
		client = new CouchbaseClient(hosts, bucket, password);
	}

	@Override
	public void updateMeasurement(DataPoint value) {
		Date timestamp = new Date(DateTimeZone.getDefault().convertUTCToLocal(
				new Date().getTime()));
		LOG.info("datapoint: " + timestamp.toString() + ", id: "
				+ value.getId() + ", type: " + value.getType() + ", vallue: "
				+ value.getValue());
		List<City> resultList = em
				.createNamedQuery("City.findBySensorId", City.class)
				.setParameter("sensorId", value.getId()).getResultList();
		String cityName = "";
		if (!resultList.isEmpty()) {
			cityName = resultList.get(0).getCityName();
		}

		String couchbaseKey = cityName + ":" + timestamp.getTime();
		Measurement measurement = new Measurement();
		measurement.updateMeasurements(value, timestamp.getTime());
		measurement.setCityName(cityName);
		client.set(couchbaseKey, measurement);
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
