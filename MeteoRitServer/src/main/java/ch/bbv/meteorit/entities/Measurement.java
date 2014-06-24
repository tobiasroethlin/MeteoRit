package ch.bbv.meteorit.entities;

import java.util.Date;

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

@Entity
@XmlRootElement
@NamedQueries({
		@NamedQuery(name = "Measurement.findAll", query = "SELECT a FROM Measurement a ORDER BY a.timestamp"),
		@NamedQuery(name = "Measurement.findById", query = "SELECT a FROM Measurement a WHERE a.id = :id ORDER BY a.timestamp DESC"),
		@NamedQuery(name = "Measurement.findBySensorId", query = "SELECT a FROM Measurement a WHERE a.sensorId = :sensorId ORDER BY a.timestamp DESC"),
		@NamedQuery(name = "Measurement.findLatestBySensorIdAndTimestamp", query = "SELECT a FROM Measurement a WHERE a.sensorId = :sensorId and a.timestamp <= :timestamp ORDER BY a.timestamp DESC"),
		@NamedQuery(name = "Measurement.findBySensorIdAndTimestamp", query = "SELECT a FROM Measurement a WHERE a.sensorId = :sensorId and a.timestamp = :timestamp") })
@Table(indexes = { @Index(columnList = "timestamp, sensorId", unique = false) })
public class Measurement {

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
	private Date timestamp;
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

	public Date getTimestamp() {
		return timestamp;
	}

	public void setTimestamp(Date timestamp) {
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

	public void updateMeasurements(DataPoint value, Date timestamp) {
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

}
