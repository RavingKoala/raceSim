﻿using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ControllerTest {
	public class Model_Stats_BesteSpeler {

		[SetUp]
		public void Setup() { }

		[Test]
		public void BesteSpeler_EmptyList() {
			Stats<StatPoints> stats = new Stats<StatPoints>();

			Assert.IsNull(stats.BesteSpeler());
		}
	}
}