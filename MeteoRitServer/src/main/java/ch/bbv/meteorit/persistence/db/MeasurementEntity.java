package ch.bbv.meteorit.persistence.db;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Index;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;
import javax.persistence.TableGenerator;
import javax.validation.constraints.NotNull;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

import ch.bbv.meteorit.bean.DataPoint;
import ch.bbv.meteorit.entities.Measurement;

@Entity
@XmlRootElement
@NamedQueries({
		@NamedQuery(name = "MeasurementEntity.findAll", query = "SELECT a FROM MeasurementEntity a ORDER BY a.timestamp"),
		@NamedQuery(name = "MeasurementEntity.findById", query = "SELECT a FROM MeasurementEntity a WHERE a.id = :id ORDER BY a.timestamp DESC"),
		@NamedQuery(name = "MeasurementEntity.findBySensorId", query = "SELECT a FROM MeasurementEntity a WHERE a.sensorId = :sensorId ORDER BY a.timestamp DESC"),
		@NamedQuery(name = "MeasurementEntity.findLatestBySensorIdAndTimestamp", query = "SELECT a FROM MeasurementEntity a WHERE a.sensorId = :sensorId and a.timestamp <= :timestamp ORDER BY a.timestamp DESC"),
		@NamedQuery(name = "MeasurementEntity.findBySensorIdAndTimestamp", query = "SELECT a FROM MeasurementEntity a WHERE a.sensorId = :sensorId and a.timestamp = :timestamp") })
@Table(indexes = { @Index(columnList = "timestamp, sensorId", unique = false) })
public class MeasurementEntity {

	@Id
	@NotNull
	@GeneratedValue(strategy = GenerationType.TABLE, generator = "measurement")
	@TableGenerator(name = "measurement", initialValue = 100, allocationSize = 50, table = "generator")
	@XmlElement(name = "persistenceId")
	private int id;

	@XmlElement(name = "Id")
	private int sensorId;
	@XmlElement(name = "CityName")
	private String cityName;
	@XmlElement(name = "Timestamp")
	private long timestamp;
	@XmlElement(name = "Temperature")
	private double temperature;
	@XmlElement(name = "Pressure")
	private double pressure;
	@XmlElement(name = "Humidity")
	private int humidity;

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public int getSensorId() {
		return sensorId;
	}

	public void setSensorId(int id) {
		this.sensorId = id;
	}

	public String getCityName() {
		return cityName;
	}

	public void setCityName(String cityName) {
		this.cityName = cityName;
	}

	public long getTimestamp() {
		return timestamp;
	}

	public void setTimestamp(long timestamp) {
		this.timestamp = timestamp;
	}

	public double getTemperature() {
		return temperature;
	}

	public void setTemperature(double temperature) {
		this.temperature = temperature;
	}

	public double getPressure() {
		return pressure;
	}

	public void setPressure(double pressure) {
		this.pressure = pressure;
	}

	public int getHumidity() {
		return humidity;
	}

	public void setHumidity(int humidity) {
		this.humidity = humidity;
	}

	public void updateMeasurements(DataPoint value, long timestamp) {
		this.timestamp = timestamp;
		this.sensorId = value.getId();
		if ("Pressure".equals(value.getType())) {
			this.pressure = value.getValue();
		} else if ("Temperature".equals(value.getType())) {
			this.temperature = value.getValue();
		} else if ("Humidity".equals(value.getType())) {
			this.humidity = value.getValue().intValue();
		}
	}
	
	public Measurement getMeasurement() {
		Measurement measurement = new Measurement();
		measurement.setCityName(cityName);
		measurement.setHumidity(humidity);
		measurement.setPressure(pressure);
		measurement.setTemperature(temperature);
		measurement.setTimestamp(timestamp);
		measurement.setId(sensorId);
		return measurement;
	}

}
