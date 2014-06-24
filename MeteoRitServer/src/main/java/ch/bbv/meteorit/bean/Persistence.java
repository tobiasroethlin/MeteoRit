package ch.bbv.meteorit.bean;

import java.util.Collections;
import java.util.HashMap;
import java.util.Map;
import java.util.TreeMap;
import java.util.logging.Logger;

import javax.inject.Singleton;

import org.joda.time.DateTime;

import ch.bbv.meteorit.entities.Measurement;
import ch.bbv.meteorit.rest.DataPointFacadeREST;

@Singleton
public class Persistence {

	private static Logger LOG = Logger.getLogger(DataPointFacadeREST.class.getName());

	private Map<Integer, Map<Long, Measurement>> measurements = new HashMap<>();

	public void updateMeasurement(DataPoint value) {
		long timestamp = DateTime.now().getMillis() / 1000;
		LOG.info("datapoint: " + timestamp + ", id: " + value.getId() + ", type: " + value.getType() + ", vallue: "
				+ value.getValue());
		if (!measurements.containsKey(value.getId())) {
			measurements.put(value.getId(), new TreeMap<Long, Measurement>(Collections.reverseOrder()));
		}
		Map<Long, Measurement> timestampMap = measurements.get(value.getId());
		if (!timestampMap.containsKey(timestamp)) {
			timestampMap.put(timestamp, new Measurement());
		}
		measurements.get(value.getId()).get(timestamp).updateMeasurements(value, timestamp);
	}

	public Measurement getMeasurement(int id, long timestamp) {
		Map<Long, Measurement> timestampMap = measurements.get(id);
		if (timestampMap != null) {
			Measurement measurement = timestampMap.get(timestamp);
			if (measurement != null) {
				return measurement;
			}
		}
		return new Measurement();
	}

	public Measurement getMeasurement(int id) {
		Map<Long, Measurement> timestampMap = measurements.get(id);
		if (timestampMap != null) {
			return timestampMap.values().iterator().next();
		}
		return new Measurement();
	}

	private class KeyObject {
		int id;
		long timestamp;

		public KeyObject(int id, long timestamp) {
			this.id = id;
			this.timestamp = timestamp;
		}

		@Override
		public int hashCode() {
			final int prime = 31;
			int result = 1;
			result = prime * result + getOuterType().hashCode();
			result = prime * result + id;
			result = prime * result + (int) (timestamp ^ (timestamp >>> 32));
			return result;
		}

		@Override
		public boolean equals(Object obj) {
			if (this == obj)
				return true;
			if (obj == null)
				return false;
			if (getClass() != obj.getClass())
				return false;
			KeyObject other = (KeyObject) obj;
			if (!getOuterType().equals(other.getOuterType()))
				return false;
			if (id != other.id)
				return false;
			if (timestamp != other.timestamp)
				return false;
			return true;
		}

		private Persistence getOuterType() {
			return Persistence.this;
		}
	}
}
