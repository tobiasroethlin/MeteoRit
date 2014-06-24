package ch.bbv.meteorit.entities;

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

@Entity
@XmlRootElement
@NamedQueries({
		@NamedQuery(name = "City.findAll", query = "SELECT a FROM City a"),
		@NamedQuery(name = "City.findById", query = "SELECT a FROM City a WHERE a.id = :id"),
		@NamedQuery(name = "City.findBySensorId", query = "SELECT a FROM City a WHERE a.sensorId = :sensorId") })
@Table(indexes = { @Index(columnList = "sensorId", unique = true) })
public class City {

	@Id
	@NotNull
	@GeneratedValue(strategy = GenerationType.TABLE, generator = "cities")
	@TableGenerator(name = "cities", initialValue = 100, allocationSize = 50, table = "generator")
	@XmlElement(name = "persistenceId")
	private int id;

	@XmlElement(name = "SensorId")
	private int sensorId;
	@XmlElement(name = "CityName")
	private String cityName;

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public int getSensorId() {
		return sensorId;
	}

	public void setSensorId(int sensorId) {
		this.sensorId = sensorId;
	}

	public String getCityName() {
		return cityName;
	}

	public void setCityName(String cityName) {
		this.cityName = cityName;
	}
}
